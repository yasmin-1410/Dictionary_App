
// module CRUD
// open FileIO

// let addWord (key:string) (value:string) =
//     let dict = readDictionary()
//     let newDict = dict.Add(key, value)
//     writeDictionary newDict

// let updateWord (key:string) (newValue:string) =
//     let dict = readDictionary()
//     if dict.ContainsKey(key) then
//         dict.Add(key,newValue) |> writeDictionary
//         true
//     else
//         false 

// let deleteWord (key:string) =
//     let dict = readDictionary()
//     if dict.ContainsKey(key) then
//         dict.Remove(key) |> writeDictionary
//         true
//     else
//         false

module CRUD
open FileIO
open System
open wordModel
let addWord (key:string) (value:string) =
    try
        let dict = readDictionary()
        let entry = { Word = key; Definition = value }
        if String.IsNullOrEmpty(key) || dict.ContainsKey(key.ToLower()) || String.IsNullOrEmpty(value) then
            false
        else
            let newDict = dict.Add(key, entry)
            writeDictionary newDict
            true
    with
    | ex ->
        printfn "Error adding word: %s" ex.Message
        false

let updateWord (key:string) (newValue:string) =
    try
        let dict = readDictionary()
        let entry = { Word = key; Definition = newValue }
        if dict.ContainsKey(key.ToLower()) && not(String.IsNullOrEmpty(newValue)) then
            let newDict = dict.Add(key, entry)
            writeDictionary newDict
            true
        else
            false
    with
    | ex ->
        printfn "Error updating word: %s" ex.Message
        false

let deleteWord (key:string) =
    try
        let dict = readDictionary()
        if dict.ContainsKey(key.ToLower()) then
            let newDict = dict.Remove(key)
            writeDictionary newDict
            true
        else
            false
    with
    | ex ->
        printfn "Error deleting word: %s" ex.Message
        false