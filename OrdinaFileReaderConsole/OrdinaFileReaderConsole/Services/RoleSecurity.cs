using System.Collections.Generic;

namespace OrdinaFileReaderConsole.Services
{
    public class RoleSecurity
    {
        private Dictionary<string, List<string>> rolePermissions;

        public RoleSecurity()
        {
            InitializeRolePermissions();
        }

        private void InitializeRolePermissions()
        {
            rolePermissions = new Dictionary<string, List<string>>
            {
                { "admin", new List<string> { ".xml", ".txt", ".json" } },
                { "user", new List<string> {".txt", } }
            };
        }

        public bool CanReadFile(string filePath, string role)
        {
            if (rolePermissions.ContainsKey(role))
            {
                string fileExtension = System.IO.Path.GetExtension(filePath);

                if (rolePermissions[role].Contains(fileExtension))
                {
                    return true;
                }
            }

            return false;
        }
    }
}