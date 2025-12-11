module CRUDTests

open Xunit
open FsUnit.Xunit
open CRUD
open FileIO
open wordModel

[<Theory>]
[<InlineData("try", "test")>]
let ``addWord adds a new key`` (word :string ,meaning :string) =
    addWord word meaning
    let dict = FileIO.readDictionary()

    let dict = FileIO.readDictionary() |> Map.toList
    dict |> should contain (word, { Word = word; Definition = meaning })


[<Theory>]
[<InlineData("test", "test22")>]
let updateWord (word :string ,meaning :string) =
    updateWord word meaning
    let dict=FileIO.readDictionary()
    let dict = FileIO.readDictionary() |> Map.toList
    dict |> should contain (word, { Word = word; Definition = meaning })


[<Theory>]
[<InlineData("word", "word")>]
let deleteWord (word :string ,meaning :string)= 
    deleteWord word
    let dict=FileIO.readDictionary()
    let dict = FileIO.readDictionary() |> Map.toList
    dict |> should not' (contain (word, { Word = word; Definition = meaning }))
    

  






