
// open System
// open System.Drawing
// open System.Windows.Forms
// open CRUD
// open FileIO
// open SearchModule
// open wordModel
// open UserRoles

// [<STAThread>]
// do
//     Application.EnableVisualStyles()

//     let font = new Font("Segoe UI", 11.0f)
//     let titleFont = new Font("Segoe UI", 14.0f, FontStyle.Bold)

//     // ======== Role Selection Form ========
//     let roleForm =
//         new Form(
//             Text = "Select Role",
//             Width = 400,
//             Height = 300,
//             BackColor = Color.White,
//             StartPosition = FormStartPosition.CenterScreen
//         )

//     let lblTitle =
//         new Label(
//             Text = "Welcome to Dictionary App",
//             Top = 30,
//             Left = 60,
//             Width = 280,
//             Height = 30,
//             Font = titleFont,
//             TextAlign = ContentAlignment.MiddleCenter
//         )

//     let lblSubtitle =
//         new Label(
//             Text = "Please select your role:",
//             Top = 70,
//             Left = 100,
//             Width = 200,
//             Font = font
//         )

//     let btnAdmin =
//         new Button(
//             Text = "Admin",
//             Top = 120,
//             Left = 100,
//             Width = 200,
//             Height = 50,
//             BackColor = Color.FromArgb(220,53,69),
//             ForeColor = Color.White,
//             FlatStyle = FlatStyle.Flat,
//             Font = titleFont
//         )
//     btnAdmin.FlatAppearance.BorderSize <- 0

//     let btnUser =
//         new Button(
//             Text = "User",
//             Top = 180,
//             Left = 100,
//             Width = 200,
//             Height = 50,
//             BackColor = Color.FromArgb(40,167,69),
//             ForeColor = Color.White,
//             FlatStyle = FlatStyle.Flat,
//             Font = titleFont
//         )
//     btnUser.FlatAppearance.BorderSize <- 0

//     let mutable selectedRole: Role option = None
//     let mutable currentUser: UserAccount option = None

//     btnAdmin.Click.Add(fun _ -> 
//         selectedRole <- Some Role.Admin
//         roleForm.DialogResult <- DialogResult.OK
//         roleForm.Close()
//     )

//     btnUser.Click.Add(fun _ -> 
//         selectedRole <- Some Role.User
//         currentUser <- Some { Username="Guest User"; Password=""; Role=Role.User }
//         roleForm.DialogResult <- DialogResult.OK
//         roleForm.Close()
//     )

//     roleForm.Controls.AddRange [| lblTitle :> Control; lblSubtitle :> Control; btnAdmin :> Control; btnUser :> Control |]

//     if roleForm.ShowDialog() = DialogResult.OK then

//         // ======== Admin Login ========
//         if selectedRole = Some Role.Admin then
//             let loginForm =
//                 new Form(
//                     Text="Admin Login",
//                     Width=400,
//                     Height=250,
//                     BackColor=Color.White,
//                     StartPosition=FormStartPosition.CenterScreen
//                 )

//             let lblUsername = new Label(Text="Username:", Top=30, Left=30, Width=100, Font=font)
//             let txtUsername = new TextBox(Top=30, Left=140, Width=200, Font=font)

//             let lblPassword = new Label(Text="Password:", Top=70, Left=30, Width=100, Font=font)
//             let txtPassword = new TextBox(Top=70, Left=140, Width=200, Font=font, UseSystemPasswordChar=true)

//             let lblStatus = new Label(Top=150, Left=30, Width=330, ForeColor=Color.Red, Font=font)

//             let btnLogin = new Button(
//                                 Text="Login",
//                                 Top=110,
//                                 Left=140,
//                                 Width=100,
//                                 Height=35,
//                                 BackColor=Color.FromArgb(0,120,215),
//                                 ForeColor=Color.White,
//                                 FlatStyle=FlatStyle.Flat,
//                                 Font=font
//                             )
//             btnLogin.FlatAppearance.BorderSize <- 0

//             btnLogin.Click.Add(fun _ ->
//                 match authenticate txtUsername.Text txtPassword.Text with
//                 | Some u when u.Role = Role.Admin ->
//                     currentUser <- Some u
//                     loginForm.DialogResult <- DialogResult.OK
//                     loginForm.Close()
//                 | Some _ ->
//                     lblStatus.Text <- "You are not an admin!"
//                 | None ->
//                     lblStatus.Text <- "Invalid username or password!"
//             )

//             loginForm.Controls.AddRange [|
//                 lblUsername :> Control
//                 txtUsername :> Control
//                 lblPassword :> Control
//                 txtPassword :> Control
//                 btnLogin :> Control
//                 lblStatus :> Control
//             |]

//             if loginForm.ShowDialog() <> DialogResult.OK then
//                 Environment.Exit(0)

//         // ======== Main Dictionary Form ========
//         let form = new Form(Text="Dictionary App", Width=650, Height=550, BackColor=Color.White)

//         let roleText =
//             match currentUser with
//             | Some u when u.Role = Role.Admin -> sprintf "Admin (%s)" u.Username
//             | Some u when u.Role = Role.User -> "User (Guest)"
//             | _ -> "Guest"

//         let lblCurrentUser =
//             new Label(
//                 Text=sprintf "Logged in as: %s" roleText,
//                 Top=5,
//                 Left=420,
//                 Width=220,
//                 Font=new Font("Segoe UI",9.0f),
//                 ForeColor=Color.FromArgb(0,120,215)
//             )

//         let makeButton(text, top, left, enabled, color) =
//             let b = new Button(
//                         Text=text,
//                         Top=top,
//                         Left=left,
//                         Width=160,
//                         Height=35,
//                         BackColor=color,
//                         ForeColor=Color.White,
//                         FlatStyle=FlatStyle.Flat,
//                         Font=font,
//                         Enabled=enabled
//                     )
//             b.FlatAppearance.BorderSize <- 0
//             b

//         let lblKey = new Label(Text="Word:", Top=30, Left=20, Font=font)
//         let txtKey = new TextBox(Top=30, Left=120, Width=200, Font=font)

//         let lblValue = new Label(Text="Meaning:", Top=70, Left=20, Font=font)
//         let txtValue = new TextBox(Top=70, Left=120, Width=200, Font=font)

//         let lst = new ListBox(Top=150, Left=20, Width=580, Height=300, Font=font)

//         let isAdmin = hasAdminRole currentUser

//         let adminColor = Color.FromArgb(220,53,69)
//         let userColor = Color.FromArgb(0,120,215)

//         let btnAdd = if isAdmin then makeButton("Add Word", 100, 20, true, adminColor) else null
//         let btnUpdate = if isAdmin then makeButton("Update Word", 100, 200, true, adminColor) else null
//         let btnDelete = if isAdmin then makeButton("Delete Word", 100, 380, true, adminColor) else null

//         let btnSearchExact = makeButton("Search Exact", 460, 20, true, userColor)
//         let btnSearchPartial = makeButton("Search Partial", 460, 200, true, userColor)
//         let btnShowAll = makeButton("Show All", 460, 380, true, userColor)

//         // ======== Admin Actions ========
//         if isAdmin then
//             btnAdd.Click.Add(fun _ ->
//                 if addWord txtKey.Text txtValue.Text then
//                     MessageBox.Show("Word added successfully!") |> ignore
//                 else
//                     MessageBox.Show("Invalid input or word already exists!") |> ignore
//             )

//             btnUpdate.Click.Add(fun _ ->
//                 if updateWord txtKey.Text txtValue.Text then
//                     MessageBox.Show("Word updated successfully!") |> ignore
//                 else
//                     MessageBox.Show("Invalid input or word not found!") |> ignore
//             )

//             btnDelete.Click.Add(fun _ ->
//                 if deleteWord txtKey.Text then
//                     MessageBox.Show("Word deleted successfully!") |> ignore
//                 else
//                     MessageBox.Show("Word not found!") |> ignore
//             )

//         // ======== Search Exact ========
//         btnSearchExact.Click.Add(fun _ ->
//             if String.IsNullOrWhiteSpace(txtKey.Text) then
//                 MessageBox.Show("Please enter a word to search!") |> ignore
//             else
//                 lst.Items.Clear()
//                 let dict: Map<string,WordEntry> = readDictionary()
//                 match searchExact txtKey.Text dict with
//                 | Some def -> lst.Items.Add(sprintf "%s -> %s" txtKey.Text def) |> ignore
//                 | None -> MessageBox.Show("Not found!") |> ignore
//         )

//         // ======== Search Partial ========
//         btnSearchPartial.Click.Add(fun _ ->
//             if String.IsNullOrWhiteSpace(txtKey.Text) then
//                 MessageBox.Show("Please enter a word to search!") |> ignore
//             else
//                 lst.Items.Clear()
//                 let dict = readDictionary()
//                 let results = searchPartial txtKey.Text dict  // List<WordEntry>
//                 if results.IsEmpty then
//                     MessageBox.Show("No matches found!") |> ignore
//                 else
//                     for entry in results do
//                         lst.Items.Add(sprintf "Word: %s , Definition: %s" entry.Word entry.Definition) |> ignore
//         )

//         // ======== Show All ========
//         btnShowAll.Click.Add(fun _ ->
//             lst.Items.Clear()
//             let dict = readDictionary()
//             if dict.IsEmpty then
//                 MessageBox.Show("Dictionary is empty!") |> ignore
//             else
//                 for (_, entry) in dict |> Map.toList do
//                     lst.Items.Add(sprintf "%s -> %s" entry.Word entry.Definition) |> ignore
//         )

//         // ======== Add controls to form ========
//         let controls =
//             [ lblCurrentUser :> Control
//               lblKey :> Control; txtKey :> Control
//               lblValue :> Control; txtValue :> Control ]
//             @ (if isAdmin then [btnAdd :> Control; btnUpdate :> Control; btnDelete :> Control] else [])
//             @ [ btnSearchExact :> Control; btnSearchPartial :> Control; btnShowAll :> Control; lst :> Control ]

//         form.Controls.AddRange(controls |> List.toArray)

//         Application.Run(form)

open System
open System.Drawing
open System.Windows.Forms
open CRUD
open FileIO
open SearchModule
open wordModel
open UserRoles

[<STAThread>]
do
    Application.EnableVisualStyles()

    let font = new Font("Segoe UI", 11.0f)
    let titleFont = new Font("Segoe UI", 14.0f, FontStyle.Bold)

    // ======== Role Selection Form ========
    let roleForm =
        new Form(
            Text = "Select Role",
            Width = 400,
            Height = 300,
            BackColor = Color.White,
            StartPosition = FormStartPosition.CenterScreen
        )

    let lblTitle =
        new Label(
            Text = "Welcome to Dictionary App",
            Top = 30,
            Left = 60,
            Width = 280,
            Height = 30,
            Font = titleFont,
            TextAlign = ContentAlignment.MiddleCenter
        )

    let lblSubtitle =
        new Label(
            Text = "Please select your role:",
            Top = 70,
            Left = 100,
            Width = 200,
            Font = font
        )

    let btnAdmin =
        new Button(
            Text = "Admin",
            Top = 120,
            Left = 100,
            Width = 200,
            Height = 50,
            BackColor = Color.FromArgb(220,53,69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = titleFont
        )
    btnAdmin.FlatAppearance.BorderSize <- 0

    let btnUser =
        new Button(
            Text = "User",
            Top = 180,
            Left = 100,
            Width = 200,
            Height = 50,
            BackColor = Color.FromArgb(40,167,69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = titleFont
        )
    btnUser.FlatAppearance.BorderSize <- 0

    let mutable selectedRole: Role option = None
    let mutable currentUser: UserAccount option = None

    btnAdmin.Click.Add(fun _ -> 
        selectedRole <- Some Role.Admin
        roleForm.DialogResult <- DialogResult.OK
        roleForm.Close()
    )

    btnUser.Click.Add(fun _ -> 
        selectedRole <- Some Role.User
        currentUser <- Some { Username="Guest User"; Password=""; Role=Role.User }
        roleForm.DialogResult <- DialogResult.OK
        roleForm.Close()
    )

    roleForm.Controls.AddRange [| lblTitle :> Control; lblSubtitle :> Control; btnAdmin :> Control; btnUser :> Control |]

    if roleForm.ShowDialog() = DialogResult.OK then

        // ======== Admin Login ========
        if selectedRole = Some Role.Admin then
            let loginForm =
                new Form(
                    Text="Admin Login",
                    Width=400,
                    Height=250,
                    BackColor=Color.White,
                    StartPosition=FormStartPosition.CenterScreen
                )

            let lblUsername = new Label(Text="Username:", Top=30, Left=30, Width=100, Font=font)
            let txtUsername = new TextBox(Top=30, Left=140, Width=200, Font=font)

            let lblPassword = new Label(Text="Password:", Top=70, Left=30, Width=100, Font=font)
            let txtPassword = new TextBox(Top=70, Left=140, Width=200, Font=font, UseSystemPasswordChar=true)

            let lblStatus = new Label(Top=150, Left=30, Width=330, ForeColor=Color.Red, Font=font)

            let btnLogin = new Button(
                                Text="Login",
                                Top=110,
                                Left=140,
                                Width=100,
                                Height=35,
                                BackColor=Color.FromArgb(0,120,215),
                                ForeColor=Color.White,
                                FlatStyle=FlatStyle.Flat,
                                Font=font
                            )
            btnLogin.FlatAppearance.BorderSize <- 0

            btnLogin.Click.Add(fun _ ->
                match authenticate txtUsername.Text txtPassword.Text with
                | Some u when u.Role = Role.Admin ->
                    currentUser <- Some u
                    loginForm.DialogResult <- DialogResult.OK
                    loginForm.Close()
                | Some _ ->
                    lblStatus.Text <- "You are not an admin!"
                | None ->
                    lblStatus.Text <- "Invalid username or password!"
            )

            loginForm.Controls.AddRange [|
                lblUsername :> Control
                txtUsername :> Control
                lblPassword :> Control
                txtPassword :> Control
                btnLogin :> Control
                lblStatus :> Control
            |]

            if loginForm.ShowDialog() <> DialogResult.OK then
                Environment.Exit(0)

        // ======== Main Dictionary Form ========
        let form = new Form(Text="Dictionary App", Width=650, Height=550, BackColor=Color.White)

        let roleText =
            match currentUser with
            | Some u when u.Role = Role.Admin -> sprintf "Admin (%s)" u.Username
            | Some u when u.Role = Role.User -> "User (Guest)"
            | _ -> "Guest"

        let lblCurrentUser =
            new Label(
                Text=sprintf "Logged in as: %s" roleText,
                Top=5,
                Left=420,
                Width=220,
                Font=new Font("Segoe UI",9.0f),
                ForeColor=Color.FromArgb(0,120,215)
            )

        let makeButton(text, top, left, enabled, color) =
            let b = new Button(
                        Text=text,
                        Top=top,
                        Left=left,
                        Width=160,
                        Height=35,
                        BackColor=color,
                        ForeColor=Color.White,
                        FlatStyle=FlatStyle.Flat,
                        Font=font,
                        Enabled=enabled
                    )
            b.FlatAppearance.BorderSize <- 0
            b

        let lblKey = new Label(Text="Word:", Top=30, Left=20, Font=font)
        let txtKey = new TextBox(Top=30, Left=120, Width=200, Font=font)

        let lblValue = new Label(Text="Meaning:", Top=70, Left=20, Font=font)
        let txtValue = new TextBox(Top=70, Left=120, Width=200, Font=font)

        // ======== إخفاء Meaning للـ User ========
        match currentUser with
        | Some u when u.Role = Role.Admin ->
            lblValue.Visible <- true
            txtValue.Visible <- true
        | _ ->
            lblValue.Visible <- false
            txtValue.Visible <- false

        let lst = new ListBox(Top=150, Left=20, Width=580, Height=300, Font=font)

        let isAdmin = hasAdminRole currentUser

        let adminColor = Color.FromArgb(220,53,69)
        let userColor = Color.FromArgb(0,120,215)

        let btnAdd = if isAdmin then makeButton("Add Word", 100, 20, true, adminColor) else null
        let btnUpdate = if isAdmin then makeButton("Update Word", 100, 200, true, adminColor) else null
        let btnDelete = if isAdmin then makeButton("Delete Word", 100, 380, true, adminColor) else null

        let btnSearchExact = makeButton("Search Exact", 460, 20, true, userColor)
        let btnSearchPartial = makeButton("Search Partial", 460, 200, true, userColor)
        let btnShowAll = makeButton("Show All", 460, 380, true, userColor)

        // ======== Admin Actions ========
        if isAdmin then
            btnAdd.Click.Add(fun _ ->
                if addWord txtKey.Text txtValue.Text then
                    MessageBox.Show("Word added successfully!") |> ignore
                else
                    MessageBox.Show("Invalid input or word already exists!") |> ignore
            )

            btnUpdate.Click.Add(fun _ ->
                if updateWord txtKey.Text txtValue.Text then
                    MessageBox.Show("Word updated successfully!") |> ignore
                else
                    MessageBox.Show("Invalid input or word not found!") |> ignore
            )

            btnDelete.Click.Add(fun _ ->
                if deleteWord txtKey.Text then
                    MessageBox.Show("Word deleted successfully!") |> ignore
                else
                    MessageBox.Show("Word not found!") |> ignore
            )

        // ======== Search Exact ========
        btnSearchExact.Click.Add(fun _ ->
            if String.IsNullOrWhiteSpace(txtKey.Text) then
                MessageBox.Show("Please enter a word to search!") |> ignore
            else
                lst.Items.Clear()
                let dict: Map<string,WordEntry> = readDictionary()
                match searchExact txtKey.Text dict with
                | Some def -> lst.Items.Add(sprintf "%s -> %s" txtKey.Text def) |> ignore
                | None -> MessageBox.Show("Not found!") |> ignore
        )

        // ======== Search Partial ========
        btnSearchPartial.Click.Add(fun _ ->
            if String.IsNullOrWhiteSpace(txtKey.Text) then
                MessageBox.Show("Please enter a word to search!") |> ignore
            else
                lst.Items.Clear()
                let dict = readDictionary()
                let results = searchPartial txtKey.Text dict
                if results.IsEmpty then
                    MessageBox.Show("No matches found!") |> ignore
                else
                    for entry in results do
                        lst.Items.Add(sprintf "Word: %s , Definition: %s" entry.Word entry.Definition) |> ignore
        )

        // ======== Show All ========
        btnShowAll.Click.Add(fun _ ->
            lst.Items.Clear()
            let dict = readDictionary()
            if dict.IsEmpty then
                MessageBox.Show("Dictionary is empty!") |> ignore
            else
                for (_, entry) in dict |> Map.toList do
                    lst.Items.Add(sprintf "%s -> %s" entry.Word entry.Definition) |> ignore
        )

        // ======== Add controls to form ========
        let controls =
            [ lblCurrentUser :> Control
              lblKey :> Control; txtKey :> Control
              lblValue :> Control; txtValue :> Control ]
            @ (if isAdmin then [btnAdd :> Control; btnUpdate :> Control; btnDelete :> Control] else [])
            @ [ btnSearchExact :> Control; btnSearchPartial :> Control; btnShowAll :> Control; lst :> Control ]

        form.Controls.AddRange(controls |> List.toArray)

        Application.Run(form)
