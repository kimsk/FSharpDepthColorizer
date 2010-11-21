//----------------------------------------------------------------------------
//
// Copyright (c) 2002-2010 Microsoft Corporation. 
//
// This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// By using this source code in any fashion, you are agreeing to be bound 
// by the terms of the Apache License, Version 2.0.
//
// You must not remove this notice, or any other, from this software.
//----------------------------------------------------------------------------

namespace Microsoft.FSharp.Compiler.AbstractIL.Internal

open Internal.Utilities
open Microsoft.FSharp.Compiler.AbstractIL 
open Microsoft.FSharp.Compiler.AbstractIL.Internal.Library 
open System.Collections.Generic

/// Sets with a specific comparison function
type internal Zset<'T> = Internal.Utilities.Collections.Tagged.Set<'T>


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module internal Zset = 

    val empty     : IComparer<'T> -> Zset<'T>
    val isEmpty  : Zset<'T> -> bool
    val contains  : 'T -> Zset<'T> -> bool
    val memberOf  : Zset<'T> -> 'T -> bool
    val add       : 'T -> Zset<'T> -> Zset<'T>
    val addList   : 'T list -> Zset<'T> -> Zset<'T>
    val addFlatList : FlatList<'T> -> Zset<'T> -> Zset<'T>
    val singleton : IComparer<'T> -> 'T -> Zset<'T>
    val remove    : 'T -> Zset<'T> -> Zset<'T>

    val count     : Zset<'T> -> int
    val union     : Zset<'T> -> Zset<'T> -> Zset<'T>
    val inter     : Zset<'T> -> Zset<'T> -> Zset<'T>
    val diff      : Zset<'T> -> Zset<'T> -> Zset<'T>
    val equal     : Zset<'T> -> Zset<'T> -> bool
    val subset    : Zset<'T> -> Zset<'T> -> bool
    val forall    : predicate:('T -> bool) -> Zset<'T> -> bool
    val exists    : predicate:('T -> bool) -> Zset<'T> -> bool
    val filter    : predicate:('T -> bool) -> Zset<'T> -> Zset<'T>   

    val fold      : ('T -> 'State -> 'State) -> Zset<'T> -> 'State -> 'State
    val iter      : ('T -> unit) -> Zset<'T> -> unit

    val elements  : Zset<'T> -> 'T list



