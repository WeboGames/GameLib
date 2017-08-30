using System;

namespace GameLib.Tests.unit.Persistence {
    [Serializable]
    public class TestData {
        public int Id;
        public float Value;
        public string Name;

        public TestData(int id, float value, string name)
        {
            Id = id;
            Value = value;
            Name = name;
        }
    }
}
