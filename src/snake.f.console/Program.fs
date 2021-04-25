module a
open System
open snake.f.console
open snake.f.console.GameTypes

let rec main gameContext =
    if gameContext.Dead then Environment.Exit 55
    Console.Clear()
    Renderer.renderMap(gameContext.Map, gameContext.Score, gameContext.StepsLeft)
    Threading.Thread.Sleep(200)
    
    if Console.KeyAvailable then
        match Console.ReadKey().Key with
        | ConsoleKey.Q -> Environment.Exit 55
        | ConsoleKey.UpArrow ->
            main(Game.gameLoop({ gameContext with Direction = Direction.Up }))
        | ConsoleKey.DownArrow ->
            main(Game.gameLoop({ gameContext with Direction = Direction.Down }))
        | ConsoleKey.LeftArrow ->
            main(Game.gameLoop({ gameContext with Direction = Direction.Left }))
        | ConsoleKey.RightArrow ->
            main(Game.gameLoop({ gameContext with Direction = Direction.Right }))
        | _ -> main(Game.gameLoop(gameContext))
    else
        main(Game.gameLoop(gameContext))
        
[<EntryPoint>]
main Game.createGameContext