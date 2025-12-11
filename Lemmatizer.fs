module Lemmatizer

let private irregular =
    dict [
        "ran", "run"
        "running", "run"
        "ate", "eat"
        "eaten", "eat"
        "saw", "see"
        "seen", "see"
        "gone", "go"
        "went", "go"
        "did", "do"
        "done", "do"
        "had", "have"
        "has", "have"
        "was", "be"
        "were", "be"
        "been", "be"
        "is", "be"
        "am", "be"
        "are", "be"


        "better", "good"
        "best", "good"
        "worse", "bad"
        "worst", "bad"

  
        "children", "child"
        "mice", "mouse"
        "geese", "goose"
        "men", "man"
        "women", "woman"
        "feet", "foot"
        "teeth", "tooth"
        "people", "person"
        "oxen", "ox"

  
        "studies", "study"
        "studying", "study"
        "studied", "study"
        "running", "run"
        "ran", "run"
        "doing", "do"
        "did", "do"
        "having", "have"
        "had", "have"
    ]

let lemmatize (word:string) =
    let w = word.ToLower()


    if irregular.ContainsKey(w) then irregular.[w]

 
    elif w.EndsWith("ing") && w.Length > 4 then
        w.Substring(0, w.Length - 3)
    elif w.EndsWith("ed") && w.Length > 3 then
        w.Substring(0, w.Length - 2)
    elif w.EndsWith("es") && w.Length > 3 then
        w.Substring(0, w.Length - 2)
    elif w.EndsWith("s") && w.Length > 2 then
        w.Substring(0, w.Length - 1)
    else
        w
