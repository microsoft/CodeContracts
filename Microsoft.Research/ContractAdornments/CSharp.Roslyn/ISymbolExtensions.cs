// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis;

    internal static class ISymbolExtensions
    {
        public static MethodKind? MethodKind(this ISymbol symbol)
        {
            IMethodSymbol methodSymbol = symbol as IMethodSymbol;
            if (methodSymbol == null)
                return null;

            return methodSymbol.MethodKind;
        }

        public static bool IsConstructor(this ISymbol symbol)
        {
            return symbol.MethodKind() == Microsoft.CodeAnalysis.MethodKind.Constructor;
        }

        public static bool IsIndexer(this ISymbol symbol)
        {
            IPropertySymbol propertySymbol = symbol as IPropertySymbol;
            if (propertySymbol == null)
                return false;

            return propertySymbol.IsIndexer;
        }

        public static ITypeSymbol ReturnType(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
            case SymbolKind.Property:
                return ((IPropertySymbol)symbol).Type;

            case SymbolKind.Method:
                return ((IMethodSymbol)symbol).ReturnType;

            default:
                return null;
            }
        }

        public static ImmutableArray<ITypeSymbol> TypeArguments(this ITypeSymbol symbol)
        {
            INamedTypeSymbol namedTypeSymbol = symbol as INamedTypeSymbol;
            if (namedTypeSymbol == null)
                return default(ImmutableArray<ITypeSymbol>);

            return namedTypeSymbol.TypeArguments;
        }

        public static ITypeSymbol ElementType(this ITypeSymbol symbol)
        {
            switch (symbol.TypeKind)
            {
            case TypeKind.Array:
                return ((IArrayTypeSymbol)symbol).ElementType;

            case TypeKind.Pointer:
                return ((IPointerTypeSymbol)symbol).PointedAtType;

            default:
                return null;
            }
        }

        public static ImmutableArray<ITypeParameterSymbol> TypeParameters(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
            case SymbolKind.Method:
                return ((IMethodSymbol)symbol).TypeParameters;

            case SymbolKind.NamedType:
                return ((INamedTypeSymbol)symbol).TypeParameters;

            default:
                return ImmutableArray<ITypeParameterSymbol>.Empty;
            }
        }

        public static ImmutableArray<IParameterSymbol> Parameters(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
            case SymbolKind.Property:
                return ((IPropertySymbol)symbol).Parameters;

            case SymbolKind.Method:
                return ((IMethodSymbol)symbol).Parameters;

            default:
                throw new NotSupportedException();
            }
        }

        public static ITypeSymbol ExplicitInterfaceImplementation(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
            case SymbolKind.Property:
                IPropertySymbol firstProperty = ((IPropertySymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                return firstProperty != null ? firstProperty.ContainingType : null;

            case SymbolKind.Method:
                IMethodSymbol firstMethod = ((IMethodSymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                return firstMethod != null ? firstMethod.ContainingType : null;

            case SymbolKind.Event:
                IEventSymbol firstEvent = ((IEventSymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                return firstEvent != null ? firstEvent.ContainingType : null;

            default:
                throw new NotSupportedException();
            }
        }
    }
}
