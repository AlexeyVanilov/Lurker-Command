using GameEngine.Core;
using GameEngine.Core.InputSystem;
using GameEngine.Core.SceneSystem;
using LurkerCommand.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LurkerCommand.Models.Scenes
{
    public sealed class GameScene : Scene
    {
        private readonly GraphicsDevice _device;
        public GameScene(GraphicsDevice device) => _device = device;
        public override void Load()
        {
            TeamManager.Init();
            Field.SetMap(this);

            Camera2D cm = new Camera2D(_device);
            SetCamera(cm);

            CameraMovement cmMovement = new CameraMovement(cm, new Vector2(Field.MapWidth / 2, Field.MapHeight / 2));
            Add(cmMovement);

            var spawnData = Field.GetBaseSpawnPoints();

            foreach (var data in spawnData)
            {
                Unit newUnit = new Unit();
                newUnit.Setup(AssetManager.GetFont("Arial"), data.position, 2);
                TeamManager.AddUnitToTeam(data.team, newUnit);
                Add(newUnit);

                CellSystem.BindUnit(Field.GetCell(data.position), newUnit);
            }
            GameUI.onUIAdd += AddUI;
            GameUI.Init(_device);
        }
        public override void Dispose()
        {
            GameUI.onUIAdd -= AddUI;
            GameUI.Dispose();
            base.Dispose();
        }
        public override void Update(GameTime gameTime)
        {
            HandleGameInput();
            TeamManager.Update(gameTime);
            base.Update(gameTime);
        }

        private void HandleGameInput()
        {
            Vector2 mouseWorld = camera.ScreenToWorld(InputManager.MousePosition);
            Cell mouseCell = Field.GetCellByWorldPos(mouseWorld);
        }
    }
}