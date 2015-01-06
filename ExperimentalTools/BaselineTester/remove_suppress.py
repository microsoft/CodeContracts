import os
import sys

if (len(sys.argv[1]) != 2):
    print 'Usage: python remove_suppress.py <root dir of project>'
    print "Point at a project and all Code Contracts suppression messages and assume()'s will dissapear"
    exit()

bench = sys.argv[1]
suppress = 0
assumes = 0
no_contracts = 0
for (root,dirs,files) in os.walk(bench):
    for f in files: 
        if f.endswith('.cs'):
            content = ''
            cs = open(root + '\\' + f)
            for line in cs:
                if 'SuppressMessage("Microsoft.Contracts' in line or \
                        'SuppressMessage( "Microsoft.Contracts' in line or \
                        'SuppressMessage ( "Microsoft.Contracts' in line or \
                        'SuppressMessage ("Microsoft.Contracts' in line:
                    line = line.replace('\n', '')
                    #print 'File %s\\%s: %s' % (root, f, line)
                    suppress += 1
                    print_next = True
                elif 'ContractVerification(false' in line or \
                        'ContractVerification (false' in line or \
                        'ContractVerification ( false' in line or \
                        'ContractVerification( false' in line:
                    line = line.replace('\n', '')
                    #print 'File %s\\%s: %s' % (root, f, line)
                    no_contracts += 1
                    print_next = True
                elif 'Contract.Assume' in line:
                    line = line.replace('\n', '')
                    #print 'File %s\\%s: %s' % (root, f, line)
                    assumes += 1
                else: content += line
            cs.close()
            cs = open(root + '\\' + f, 'w')
            cs.write(content)
            cs.close()
            
print "%d supressions removed" % suppress
print "%d no verify's removed" % no_contracts
print "%d assume's removed" % assumes

