namespace GameEngine.Models {
    public abstract class GameObject {
        private bool isActive = true;
        public readonly bool isStatic;
        public bool IsActive {
            get => isActive;
            set {
                if(isActive == value) return;
                isActive = value;

                OnToggled(isActive);
            }
        }
        public GameObject(bool isStatic = false) {
            this.isStatic = isStatic;
        }
        public abstract void OnToggled(bool value);
    }
}
