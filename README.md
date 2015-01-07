CodeContracts
=============

Source code for the CodeContracts tools for .NET

Code Contracts provide a language-agnostic way to express coding assumptions in .NET programs.

The contracts take the form of pre-conditions, post-conditions, and object invariants. Contracts act as checked documentation of your external and internal APIs. The contracts are used to improve testing via runtime checking, enable static contract verification, and documentation generation. Code Contracts bring the advantages of design-by-contract programming to all .NET programming languages. We currently provide three tools:
Runtime Checking. Our binary rewriter modifies a program by injecting the contracts, which are checked as part of program execution. Rewritten programs improve testability: each contract acts as an oracle, giving a test run a pass/fail indication. 

Automatic testing tools, such as Pex, take advantage of contracts to generate more meaningful unit tests by filtering out meaningless test arguments that don't satisfy the pre-conditions. 

Static Checking. Our static checker can decide if there are any contract violations without even running the program! It checks for implicit contracts, such as null dereferences and array bounds, as well as the explicit contracts. 

Documentation Generation. Our documentation generator augments existing XML doc files with contract information. There are also new style sheets that can be used with Sandcastle so that the generated documentation pages have contract sections. 


==== How to Build the tools
Open CodeContracts.sln
Build
