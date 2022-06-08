using System.Collections.Generic;
using UnityEngine;
public class CommandManager
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    public bool CanUndo => !(undoStack.Count == 0);
    public bool CanRedo => !(redoStack.Count == 0);


    public void Execute(ICommand command)
    {
        command.Execute();
        Debug.Log(command);
        undoStack.Push(command);
    }

    public void Undo()
    {

        var emptyCommand = undoStack.Pop();

        while (CanUndo && undoStack.Peek().GetType() != typeof(EmptyCommand))
        {
            var command = undoStack.Pop();
           
            redoStack.Push(command);

            command.Undo();
        }

        redoStack.Push(emptyCommand);
    }


    public void Redo()
    {
        var emptyCommand = redoStack.Pop();

        while (CanRedo && redoStack.Peek().GetType() != typeof(EmptyCommand))
        {
            var command = redoStack.Pop();
            undoStack.Push(command);
            command.Execute();
        }

        undoStack.Push(emptyCommand);
    }

    public void EndTurn()
    {
        var emptyCommand = new EmptyCommand();
        undoStack.Push(emptyCommand);

        CleanRedoCommands();
    }

    public void CleanRedoCommands()
    {
        redoStack.Clear();
    }

}

