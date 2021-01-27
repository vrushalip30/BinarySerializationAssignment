using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.OData.Edm;

namespace BinarySerializationAssignment
{
    [Serializable]
    class BinarySerialize:IDeserializationCallback
    {
        DateTime dob = Convert.ToDateTime("1998/12/30");

        [NonSerialized]
        int age=0;
        public void OnDeserialization(object sender)
        {
            age = Date.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age = age - 1;
            Console.WriteLine("Age is : " +age);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {


            BinarySerialize bsobj = new BinarySerialize();
            FileStream fs = new FileStream(@"Agecalculation.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, bsobj);
            fs.Seek(0, SeekOrigin.Begin);
            BinarySerialize result = (BinarySerialize)bf.Deserialize(fs);
           

        }
    }
}
