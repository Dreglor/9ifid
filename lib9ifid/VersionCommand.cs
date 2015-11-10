using System.Reflection;

namespace lib9ifid
{
    public class VersionCommand : Command
    {
        public VersionCommand(object[] parameters) : base("Version", parameters, 0)
        {
        }

        public override string Exec()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}