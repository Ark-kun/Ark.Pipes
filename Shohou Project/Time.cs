using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ark.Pipes;

namespace Ark.XNA {
    public class Time : Provider<float> {
        DateTime _startTime;

        public Time(){
            _startTime = DateTime.Now;
        }

        public Time(DateTime startTime) {
            _startTime = startTime;
        }

        public override float Value {
            get {
                return (float)((DateTime.Now - _startTime).TotalMilliseconds);
            }
        }
    }
}