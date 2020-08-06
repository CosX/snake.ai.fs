module a
open System
open snake.f.console
open snake.f.console.GameTypes

let rec main(map, direction, pos, alive) =
    if alive then ()
    Console.Clear()
    Renderer.renderMap(map)
    Threading.Thread.Sleep(400)
    
    if Console.KeyAvailable then
        match Console.ReadKey().Key with
        | ConsoleKey.Q -> ()
        | ConsoleKey.UpArrow ->
            main(Game.gameLoop(map, Direction.Up, pos))
        | ConsoleKey.DownArrow ->
            main(Game.gameLoop(map, Direction.Down, pos))
        | ConsoleKey.LeftArrow ->
            main(Game.gameLoop(map, Direction.Left, pos))
        | ConsoleKey.RightArrow ->
            main(Game.gameLoop(map, Direction.Right, pos))
        | _ -> main(Game.gameLoop(map, direction, pos))
    else
        main(Game.gameLoop(map, direction, pos))
        
[<EntryPoint>]
main(Game.createGameContext)