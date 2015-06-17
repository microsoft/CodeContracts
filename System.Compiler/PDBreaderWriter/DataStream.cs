// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#if UseSingularityPDB

///////////////////////////////////////////////////////////////////////////////
//
//  Microsoft Research Singularity PDB Info Library
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//
//  File:   DataStream.cs
//
using System;
using System.IO;

namespace Microsoft.Singularity.PdbInfo.Features
{
    public class DataStream
    {
        public DataStream()
        {
            this.contentSize = 0;
            this.pages = null;
        }

        internal DataStream(int contentSize, BitAccess bits, int count)
        {
            this.contentSize = contentSize;
            if (count > 0) {
                this.pages = new int[count];
                bits.ReadInt32(this.pages);
            }
        }

        public void Read(PdbReader reader, BitAccess bits)
        {
            bits.MinCapacity(contentSize);
            Read(reader, 0, bits.Buffer, 0, contentSize);
        }

        public void Read(PdbReader reader, int position,
                         byte[] bytes, int offset, int data)
        {
            if (position + data > contentSize) {
                throw new PdbException("DataStream can't read off end of stream. " +
                                       "(pos={0},siz={1})",
                                       position, data);
            }
            if (position == contentSize) {
                return;
            }

            int left = data;
            int page = position / reader.pageSize;
            int rema = position % reader.pageSize;

            // First get remained of first page.
            if (rema != 0) {
                int todo = reader.pageSize - rema;
                if (todo > left) {
                    todo = left;
                }

                reader.Seek(pages[page], rema);
                reader.Read(bytes, offset, todo);

                offset += todo;
                left -= todo;
                page++;
            }

            // Now get the remaining pages.
            while (left > 0) {
                int todo = reader.pageSize;
                if (todo > left) {
                    todo = left;
                }

                reader.Seek(pages[page], 0);
                reader.Read(bytes, offset, todo);

                offset += todo;
                left -= todo;
                page++;
            }
        }

        internal void Write(PdbWriter writer, byte[] bytes)
        {
            Write(writer, bytes, bytes.Length);
        }

        public void Write(PdbWriter writer, byte[] bytes, int data)
        {
            if (bytes == null || data == 0) {
                return;
            }

            int left = data;
            int used = 0;
            int rema = contentSize % writer.pageSize;
            if (rema != 0) {
                int todo = writer.pageSize - rema;
                if (todo > left) {
                    todo = left;
                }

                int lastPage = pages[pages.Length - 1];
                writer.Seek(lastPage, rema);
                writer.Write(bytes, used, todo);
                used += todo;
                left -= todo;
            }

            if (left > 0) {
                int count = (left + writer.pageSize - 1) / writer.pageSize;
                int page0 = writer.AllocatePages(count);

                writer.Seek(page0, 0);
                writer.Write(bytes, used, left);

                AddPages(page0, count);
            }

            contentSize += data;
        }

        private void AddPages(int page0, int count)
        {
            if (pages == null) {
                pages = new int [count];
                for (int i = 0; i < count; i++) {
                    pages[i] = page0 + i;
                }
            }
            else {
                int[] old = pages;
                int used = old.Length;

                pages = new int [used + count];
                Array.Copy(old, pages, used);
                for (int i = 0; i < count; i++) {
                    pages[used + i] = page0 + i;
                }
            }
        }

        public int Pages {
            get { return pages == null ? 0 : pages.Length; }
        }

        public int Length {
            get { return contentSize; }
        }

        public int GetPage(int index)
        {
            return pages[index];
        }

        internal int   contentSize;
        internal int[] pages;
    }
}
#endif