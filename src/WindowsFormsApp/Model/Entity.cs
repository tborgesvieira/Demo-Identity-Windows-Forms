using System;
using System.Collections.Generic;

namespace WindowsFormsApp.Model
{
    public class Entity
    {
        public Entity()
        {
            Success = false;
        }

        public Guid Id => Guid.NewGuid();

        public bool Success { get; set; }
        public List<string> ErrorMenssage { get; set; }

        public void DefinirRetorno(bool success)
        {
            Success = success;
        }

        public void DefinirRetorno(bool success, List<string> menssage)
        {
            DefinirRetorno(success);

            ErrorMenssage = !success ? menssage : null;
        }
    }
}
