module TestData

open wordModel
open System



let sampleDict : Map<string, WordEntry> =
    Map.empty
    |> Map.add "apple" { Word = "apple"; Definition = "is a fruit" }
    |> Map.add "book" { Word = "book"; Definition = "book" }
    |> Map.add "notebook" { Word = "notebook"; Definition = "notebook" }