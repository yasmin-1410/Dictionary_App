
module UserRoles
open System.IO
open System.Text.Json



type UserAccount = {
    Username: string
    Password: string
}

let usersFilePath = "C:\\Users\\DELL\\OneDrive\\Desktop\\pl3_Project\\pl3_Project\\dictionaryApp\\users.json"

let readUsers () =
    if File.Exists usersFilePath then
        let json = File.ReadAllText(usersFilePath)
        JsonSerializer.Deserialize<UserAccount list>(json)
    else
        []  


let authenticate (username: string) (password: string) =
    readUsers()
    |> List.tryFind (fun u ->
        u.Username = username &&
        u.Password = password 
    )

