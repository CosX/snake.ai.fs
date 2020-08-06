module a
open System
open snake.f.console
open snake.f.console.GameTypes

let rec main(map, direction, pos, score, dead) =
    if dead then Environment.Exit 55
    Console.Clear()
    Renderer.renderMap(map, score)
    Threading.Thread.Sleep(200)
    
    if Console.KeyAvailable then
        match Console.ReadKey().Key with
        | ConsoleKey.Q -> Environment.Exit 55
        | ConsoleKey.UpArrow ->
            main(Game.gameLoop(map, Direction.Up, pos, score))
        | ConsoleKey.DownArrow ->
            main(Game.gameLoop(map, Direction.Down, pos, score))
        | ConsoleKey.LeftArrow ->
            main(Game.gameLoop(map, Direction.Left, pos, score))
        | ConsoleKey.RightArrow ->
            main(Game.gameLoop(map, Direction.Right, pos, score))
        | _ -> main(Game.gameLoop(map, direction, pos, score))
    else
        main(Game.gameLoop(map, direction, pos, score))
        
[<EntryPoint>]
main(Game.createGameContext)