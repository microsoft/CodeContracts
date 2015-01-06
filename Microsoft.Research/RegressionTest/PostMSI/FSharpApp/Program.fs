// Learn more about F# at http://fsharp.net
open System.Diagnostics.Contracts

let f x = 
  Contract.Requires(x > 0);
  x
;;

let g x = 
  Contract.Requires<System.Exception>(x > 0);
  x
;;

ignore (g 0)


