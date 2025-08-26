using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public abstract class Scene
    {
        public abstract void Load();
        public abstract void Update(float deltatime);
        public abstract void Draw();
        public abstract void Unload();
    }
}
