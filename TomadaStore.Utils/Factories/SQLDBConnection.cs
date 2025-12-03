using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{
                //Criador de novos objetos
    public class SQLDBConnection : DBConnectionFactory
    {
                            //a interface me permite criar objetos de diferentes tipos
        public override IDBConnection CreateDBConnection()
        {
            return new SQLDBConnectionImpl();  
        }
    }
}
