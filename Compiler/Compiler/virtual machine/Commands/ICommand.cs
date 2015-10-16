namespace Compiler.virtual_machine.Commands
{
    public interface ICommand
    {
        void Execute(string[] arguments, VirtualMachine vm);
    }
}
