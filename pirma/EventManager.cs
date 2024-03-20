using System;

namespace pirma
{
    public class EventManager
    {
        public event EventHandler<ModuleInsertedEventArgs> ModuleInserted;
        public void OnModuleInserted(string moduleName, string credits)
        {
            ModuleInserted?.Invoke(this, new ModuleInsertedEventArgs(moduleName, credits));
        }
    }
    public class StudentSelectedEventArgs : EventArgs
    {
        public string StudentNumber { get; private set; }

        public StudentSelectedEventArgs(string studentNumber)
        {
            this.StudentNumber = studentNumber;
        }
    }

    public class ModuleInsertedEventArgs : EventArgs
    {
        public string ModuleName { get; private set; }
        public string Credits { get; private set; }

        public ModuleInsertedEventArgs(string moduleName, string credits)
        {
            this.ModuleName = moduleName;
            this.Credits = credits;
        }
    }
}