module snake.f.console.GameTypes

type GameObject = Ground | Snake | Food

type Direction = Up | Down | Left | Right | None

type GameContext = {
    Map: (GameObject * Direction) [,];
    Direction: Direction;
    Position: int * int;
    Score: int;
    StepsLeft: int;
    Dead: bool;
}