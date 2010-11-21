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

// LexBuffers are for use with automatically generated lexical analyzers,
// in particular those produced by 'fslex'.

namespace Internal.Utilities.Text.Lexing

open System.Collections.Generic
open Microsoft.FSharp.Core
open Microsoft.FSharp.Control

/// Position information stored for lexing tokens
type internal Position = 
    { /// The file name index for the position, use fileOfFileIndex in range.fs to decode
      posFileIndex: int;
      /// The line number for the position
      posLineNum: int;
      /// The line number for the position in the original source file
      posOriginalLineNum : int;
      /// The absolute offset of the beginning of the line
      posStartOfLineOffset: int;
      /// The absolute offset of the column for the position
      posColumnOffset: int; }
     /// The file index for the file associated with the input stream, use fileOfFileIndex in range.fs to decode
     member FileIndex : int
     /// The line number in the input stream, assuming fresh positions have been updated 
     /// for the new line by modifying the EndPos property of the LexBuffer.
     member Line : int
     /// The line number for the position in the input stream, assuming fresh positions have been updated 
     /// using for the new line
     member OriginalLine : int
     [<System.ObsoleteAttribute("Use the AbsoluteOffset property instead")>]
     member Char : int
     /// The character number in the input stream
     member AbsoluteOffset : int
     /// Return absolute offset of the start of the line marked by the position
     member StartOfLineAbsoluteOffset : int
     /// Return the column number marked by the position, i.e. the difference between the AbsoluteOffset and the StartOfLineAbsoluteOffset
     member Column : int
     // Given a position just beyond the end of a line, return a position at the start of the next line
     member NextLine : Position     
     
     /// Given a position at the start of a token of length n, return a position just beyond the end of the token
     member EndOfToken: n:int -> Position
     /// Gives a position shifted by specified number of characters
     member ShiftColumnBy: by:int -> Position
     
     /// Get an arbitrary position, with the empty string as filename, and  
     static member Empty : Position
    
[<Sealed>]
/// Input buffers consumed by lexers generated by <c>fslex.exe </c>
type internal LexBuffer<'Char> =
    /// The start position for the lexeme
    member StartPos: Position with get,set
    /// The end position for the lexeme
    member EndPos: Position with get,set
    /// The matched string 
    member Lexeme: 'Char []
    
    /// Fast helper to turn the matched characters into a string, avoiding an intermediate array
    static member LexemeString : LexBuffer<char> -> string
    
    /// Dynamically typed, non-lexically scoped parameter table
    member BufferLocalStore : IDictionary<string,obj>
    
    /// True if the refill of the buffer ever failed , or if explicitly set to true.
    member IsPastEndOfStream: bool with get,set

    /// Create a lex buffer suitable for Unicode lexing that reads characters from the given array
    static member FromChars: char[] -> LexBuffer<char>
    /// Create a lex buffer that reads character or byte inputs by using the given function
    static member FromFunction: ('Char[] * int * int -> int) -> LexBuffer<'Char>

/// The type of tables for an unicode lexer generated by fslex. 
[<Sealed>]
type internal UnicodeTables =
    static member Create : uint16[] array * uint16[] -> UnicodeTables
    /// Interpret tables for a unicode lexer generated by fslex. 
    member Interpret:  initialState:int * LexBuffer<char> -> int

