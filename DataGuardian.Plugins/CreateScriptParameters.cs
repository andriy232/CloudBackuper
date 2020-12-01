namespace DataGuardian.Impl
{
    public class CreateScriptParameters
    {
        public string Name { get; }
        public string TargetPath { get; }

        public CreateScriptParameters(string name, string targetPath)
        {
            Name = name;
            TargetPath = targetPath;
        }
    }
}