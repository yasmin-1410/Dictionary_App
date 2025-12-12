
// module FileIO
// open System.IO
// open System.Text.Json

// let filePath = "D:\\pl3_Project\\dictionaryApp\\dictionary.json"

// let readDictionary () =
//     if File.Exists filePath then
//         let json = File.ReadAllText(filePath)
//         JsonSerializer.Deserialize<Map<string,string>>(json)
//     else
//         Map.empty

// let writeDictionary (dict: Map<string,string>) =
//     let json = JsonSerializer.Serialize(dict, JsonSerializerOptions(WriteIndented = true))
//     File.WriteAllText(filePath, json)

module FileIO
open System
open System.IO
open System.Text.Json
open wordModel
let filePath = "D:\\pl3_Project\\dictionaryApp\\dictionary.json"

let readDictionary () =
    try
        if File.Exists filePath then
            let json = File.ReadAllText(filePath)
            JsonSerializer.Deserialize<Map<string,WordEntry>>(json)
        else
            Map.empty
    with
    | :? IOException as ex ->
        printfn "IO Error while reading file: %s" ex.Message
        Map.empty
    | :? UnauthorizedAccessException as ex ->
        printfn "Access denied: %s" ex.Message
        Map.empty
    | :? JsonException as ex ->
        printfn "Invalid JSON: %s" ex.Message
        Map.empty

let writeDictionary (dict: Map<string,WordEntry>) =
    try
        let json = JsonSerializer.Serialize(dict, JsonSerializerOptions(WriteIndented = true))
        File.WriteAllText(filePath, json)
    with
    | :? IOException as ex ->
        printfn "IO Error while writing file: %s" ex.Message
    | :? UnauthorizedAccessException as ex ->
        printfn "Access denied: %s" ex.Message