module snake.f.console.Renderer

open snake.f.console.GameTypes

let renderMap (map : (GameObject * Direction)[,], score : int, steps : int) =
    for x = 0 to Array2D.length2 map - 1 do
        for y = 0 to Array2D.length1 map - 1 do
            let obj, _ = map.[x, y]
            match obj with
            | GameObject.Snake -> printf "S"
            | GameObject.Food -> printf "F"
            | GameObject.Ground -> printf "."
        printf "\n"
    printfn $"Score: %d{score}"
    printfn $"Steps left: %d{steps}"