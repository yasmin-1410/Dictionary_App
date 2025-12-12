
module SearchModule

open wordModel
open Lemmatizer

let searchExact (word: string) (dictionary: Map<string, WordEntry>) =
    let lemma = lemmatize word
    match dictionary |> Map.tryFind (lemma.ToLower()) with
    | Some entry -> Some entry.Definition
    | None -> None


// let searchPartial (sub:string) (dictionary: Map<string,WordEntry>) =
//     dictionary
//     |> Map.toList
//     |> List.filter (fun (k,_) -> k.ToLower().Contains(sub.ToLower()))



let searchPartial (sub:string) (dictionary: Map<string,WordEntry>) =
    dictionary
    |> Map.toList
    |> List.filter (fun (k,_) -> k.ToLower().Contains(sub.ToLower()))
    |> List.map (fun (_, entry) -> entry)
