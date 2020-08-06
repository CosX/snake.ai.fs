module snake.f.console.Game

open snake.f.console.GameTypes

let rec findTail(map : (GameObject * Direction)[,], (x, y) : (int * int)) =
    let (_, dir) = map.[x, y]
    let (newX, newY) =
        match dir with
        | Direction.Up -> (x + 1, y)
        | Direction.Down -> (x - 1, y)
        | Direction.Left -> (x, y + 1)
        | Direction.Right -> (x, y - 1)
        | _ -> (x, y)
    
    let (obj, _) = map.[newX, newY]
    if obj = GameObject.Snake then
        findTail(map, (newX, newY))
    else
        (x, y)

let rec createFood (map : (GameObject * Direction)[,]) =
    let rnd = System.Random()
    let x = rnd.Next(0, 18)
    let y = rnd.Next(0, 18)
    let (obj, _) = map.[x, y]
    if obj = GameObject.Snake then
        createFood(map)
    else
        (x, y)

let createGameContext =
    let map = Array2D.init 18 18 (fun _ _ -> (GameObject.Ground, Direction.None))
    map.[8, 8] <- (GameObject.Snake, Direction.Up)
    let (foodX, foodY) = createFood(map)
    map.[foodX, foodY] <- (GameObject.Food, Direction.None)
    (map, Direction.Up, (8, 8), true)
    
let modifyHitFood (map : (GameObject * Direction)[,], (x, y) : (int * int), direction : Direction) =
    map.[x, y] <- (GameObject.Snake, direction)
    let (foodX, foodY) = createFood(map)
    map.[foodX, foodY] <- (GameObject.Food, Direction.None)
    map

let modifyHitGround (map : (GameObject * Direction)[,], (x, y) : (int * int), direction : Direction) =
    map.[x, y] <- (GameObject.Snake, direction)
    let (tailX, tailY) = findTail(map, (x, y))
    map.[tailX, tailY] <- (GameObject.Ground, Direction.None)
    map

let gameLoop (map : (GameObject * Direction)[,], direction : Direction, (x, y) : (int * int)) =
    let (newX, newY) =
        match direction with
        | Direction.Up -> (x - 1, y)
        | Direction.Down -> (x + 1, y)
        | Direction.Left -> (x, y - 1)
        | Direction.Right -> (x, y + 1)
        | _ -> (x, y)
    
    try
        let (obj, _) = map.[newX, newY]
        match obj with
        | GameObject.Snake -> (map, direction, (newX, newY), false)
        | GameObject.Food -> (modifyHitFood(map, (newX, newY), direction), direction, (newX, newY), true)
        | GameObject.Ground -> (modifyHitGround(map, (newX, newY), direction), direction, (newX, newY), true)
    with
    | :? System.IndexOutOfRangeException -> (map, direction, (newX, newY), false)
      