using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Enums
{
    public enum ECategory
    {
        Rodents, //מכרסמים
        Reptiles, //זוחלים
        CatsAndDogs,
        Birds,
        Fish
    };

    public enum EStatus
    {
        Done,//אושר
        Sent,//נשלח
        Provided//סופק
    };
   
}
