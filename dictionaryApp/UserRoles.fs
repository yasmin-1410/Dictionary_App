
module UserRoles
open System.IO
open System.Text.Json

type Role =
    | Admin = 0
    | User = 1

type UserAccount = {
    Username: string
    Password: string
    Role: Role
}

let usersFilePath = "D:\\pl3_Project\\dictionaryApp\\users.json"

let readUsers () =
    if File.Exists usersFilePath then
        let json = File.ReadAllText(usersFilePath)
        JsonSerializer.Deserialize<UserAccount list>(json)
    else
        let defaultAdmin = { Username = "admin"; Password = "admin123"; Role = Role.Admin }
        let defaultUser = { Username = "user"; Password = "user123"; Role = Role.User }
        [defaultAdmin; defaultUser]

let writeUsers (users: UserAccount list) =
    let json = JsonSerializer.Serialize(users, JsonSerializerOptions(WriteIndented = true))
    File.WriteAllText(usersFilePath, json)

let authenticate (username: string) (password: string) =
    let users = readUsers()
    users |> List.tryFind (fun u -> u.Username = username && u.Password = password)

let hasAdminRole (user: UserAccount option) =
    match user with
    | Some u when u.Role = Role.Admin -> true
    | _ -> false

let isLoggedIn (user: UserAccount option) =
    user.IsSome
