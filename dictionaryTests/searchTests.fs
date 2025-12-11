module SearchTests

open Xunit
open FsUnit.Xunit
open wordModel
open SearchModule
open TestData
open FileIO

let dict = FileIO.readDictionary ()

[<Theory>]
[<InlineData("book", "electro")>]
let ``searchExact returns Some meaning if word exists`` (word: string, expected: string) =
    let result = searchExact word dict
    result |> should equal (Some expected)

[<Fact>]
let ``searchExact returns None if word does not exist`` () =
    let result = searchExact "phone" dict
    result |> should equal None

[<Theory>]
[<InlineData("book")>]
let ``searchPartial finds all keys containing substring`` (sub: string) =
    let result = searchPartial sub dict
    let expected : WordEntry list = [
        { Word = "book"; Definition = "book" }
        { Word = "notebook"; Definition = "notebook" }
    ]
    Set.ofList result |> should equal (Set.ofList expected)
