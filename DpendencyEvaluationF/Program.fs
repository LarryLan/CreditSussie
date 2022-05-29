// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO
open DependencyEvaluation.Model
open System.Collections.Generic

let Evaluate (graph : Dictionary<int, Vertex>) : double =
    let mem = Dictionary<int, double>()
    let rec EvaluateCore index : double =
        let instruction = graph.Item(index)
        match mem.ContainsKey(instruction.Index) with
            | true -> mem.Item(instruction.Index)
            | false -> match instruction.Operator with
                        | Operator.Value ->
                            let value = instruction.Value.GetValueOrDefault()
                            mem.Add(instruction.Index, instruction.Value.GetValueOrDefault())
                            value
                        | Operator.Add ->
                            let value = instruction.Dependents |> Array.map (fun x -> EvaluateCore x) |> Array.sum
                            mem.Add(instruction.Index, value)
                            value
                        | Operator.Mult ->
                            let value = instruction.Dependents |> Array.map (fun x -> EvaluateCore x) |> Array.reduce (fun x y -> x * y)
                            mem.Add(instruction.Index, value)
                            value
                        | _ -> 0.0
    let head = graph |> Seq.head // the instruction for calculation
    EvaluateCore head.Key

[<EntryPoint>]
let main argv =
    let converter = TextToVertexConverter()
    let result = File.ReadAllLines "input.txt" |> converter.ConvertToVertex |> Evaluate
    printfn "%f" result
    0 // return an integer exit code
