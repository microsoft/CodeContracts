' Copyright (c) Microsoft. All rights reserved.
' Licensed under the MIT license. See LICENSE file in the project root for full license information.

'
'  This file is included when building a contract declarative assembly
'  in order to mark it as such for recognition by the tools
'
<Assembly: ContractDeclarativeAssemblyAttribute()>

<System.Diagnostics.Contracts.ContractVerification(False)>
NotInheritable Class ContractDeclarativeAssemblyAttribute
    Inherits Global.System.Attribute
End Class

