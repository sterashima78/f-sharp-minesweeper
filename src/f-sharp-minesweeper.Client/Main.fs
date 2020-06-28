module FSharpMinesweeper.Client.Main

open Elmish
open Bolero
open Bolero.Html
open Bolero.Remoting.Client
open Bolero.Templating.Client

/// Routing endpoints definition.
type Page =
    | [<EndPoint "/">] Counter

/// The Elmish application's model.
type Model =
    {
        Page: Page
        Counter: int
    }

let initModel =
    {
        Page = Counter
        Counter = 0
    }

/// The Elmish application's update messages.
type Message =
    | SetPage of Page
    | Increment
    | Decrement
    | SetCounter of int

let update message model =
    match message with
    | SetPage page ->
        { model with Page = page }, Cmd.none

    | Increment ->
        { model with Counter = model.Counter + 1 }, Cmd.none
    | Decrement ->
        { model with Counter = model.Counter - 1 }, Cmd.none
    | SetCounter value ->
        { model with Counter = value }, Cmd.none

/// Connects the routing system to the Elmish application.
let router = Router.infer SetPage (fun model -> model.Page)

type Main = Template<"wwwroot/main.html">

let counterPage model dispatch =
    Main.Counter()
        .Decrement(fun _ -> dispatch Decrement)
        .Increment(fun _ -> dispatch Increment)
        .Value(model.Counter, SetCounter >> dispatch)
        .Elt()

let view model dispatch =
    Main()
        .Body(
            cond model.Page <| function
            | Counter -> counterPage model dispatch
        )
        .Elt()

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        Program.mkProgram (fun _ -> initModel, Cmd.none) update view
        |> Program.withRouter router

