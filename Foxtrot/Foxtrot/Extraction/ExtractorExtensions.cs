using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal static class ExtractorExtensions
    {
        public static TypeNode FindShadow(this TypeNode typeNode, AssemblyNode shadowAssembly)
        {
            if (typeNode.DeclaringType != null)
            {
                // nested type
                var parent = typeNode.DeclaringType.FindShadow(shadowAssembly);
                
                if (parent == null) return null;
                
                return parent.GetNestedType(typeNode.Name);
            }
            
            // namespace type
            return shadowAssembly.GetType(typeNode.Namespace, typeNode.Name);
        }

        public static Method FindShadow(this Method method, AssemblyNode shadowAssembly)
        {
            var shadowParent = method.DeclaringType.FindShadow(shadowAssembly);

            if (shadowParent == null) return null;

            return shadowParent.FindShadow(method);
        }

        /// <summary>
        /// Find shadow method in given type corresponding to method.
        /// Note: argument types might not match by identity. We have to match them by name.
        /// </summary>
        public static Method FindShadow(this TypeNode parent, Method method)
        {
            int dummyIndex;
            return FindShadow(parent, method, out dummyIndex);
        }

        /// <summary>
        /// Find shadow field in given type corresponding to field.
        /// </summary>
        public static Field FindShadow(this TypeNode parent, Field field)
        {
            if (field == null || field.Name == null)
            {
                return null;
            }

            MemberList members = parent.GetMembersNamed(field.Name);

            for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
            {
                Field f = members[i] as Field;
                if (f != null) return f;
            }

            return null;
        }

        /// <summary>
        /// Find shadow property in given type corresponding to property.
        /// </summary>
        public static Property FindShadow(this TypeNode parent, Property property)
        {
            if (property == null || property.Name == null)
            {
                return null;
            }

            MemberList members = parent.GetMembersNamed(property.Name);
            for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
            {
                var p = members[i] as Property;
                if (p == null) continue;

                if (p.Parameters.MatchShadow(property.Parameters))
                {
                    return p;
                }
            }

            return null;
        }

        /// <summary>
        /// Find shadow event in given type corresponding to event.
        /// </summary>
        public static Event FindShadow(this TypeNode parent, Event evnt)
        {
            if (evnt == null || evnt.Name == null)
            {
                return null;
            }

            MemberList members = parent.GetMembersNamed(evnt.Name);
            for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
            {
                var e = members[i] as Event;
                if (e == null) continue;

                return e;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="method"></param>
        /// <param name="index">if the result is nonnull, this is the index of the method within
        /// the memberlist returned by parent.GetMembersNamed(method.Name)</param>
        /// <returns></returns>
        public static Method FindShadow(this TypeNode parent, Method method, out int index)
        {
            if (method == null || method.Name == null)
            {
                index = 0;
                return null;
            }

            MemberList members = parent.GetMembersNamed(method.Name);
            for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
            {
                Method m = members[i] as Method;
                if (m == null) continue;

                if ((m.TemplateParameters == null) != (method.TemplateParameters == null)) continue;

                if (m.Parameters.MatchShadow(method.Parameters)
                    && m.ReturnType.MatchesShadow(method.ReturnType)
                    && (((m.TemplateParameters == null) && (method.TemplateParameters == null))
                        || (m.TemplateParameters.Count == method.TemplateParameters.Count)))
                {
                    index = i;
                    return m;
                }
            }

            index = 0;
            return null;
        }

        public static bool MatchShadow(this ParameterList shadow, ParameterList parameters)
        {
            ParameterList pars = shadow;

            int n = pars == null ? 0 : pars.Count;
            int m = parameters == null ? 0 : parameters.Count;
            if (n != m) return false;

            if (parameters == null) return true;

            for (int i = 0; i < n; i++)
            {
                Parameter par1 = pars[i];
                Parameter par2 = parameters[i];
                if (par1 == null || par2 == null) return false;

                if (par1.Type == null || par2.Type == null) return false;

                if (!par1.Type.MatchesShadow(par2.Type)) return false;
            }

            return true;
        }

        public static bool MatchesShadow(this TypeNode type, TypeNode that)
        {
            if (type == that) return true;

            // recurse structurally. For now, we cheat and just use the string names
            if (type.IsStructurallyEquivalentTo(that)) return true;
            
            return type.FullName == that.FullName;
        }
    }
}