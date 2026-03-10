using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;

namespace MSCMenuEscape
{
    public class MSCMenuEscape : Mod
    {
        public override string ID => "MSCMenuEscape";
        public override string Name => "Esc to Close Menu";
        public override string Author => "teamteppy";
        public override string Version => "1.0";
        public override string Description => "Press Esc to close the in-game menu";
        public override Game SupportedGames => Game.MySummerCar; 

        private PlayMakerGlobals globals;
        private GameObject optionsMenu;
        private bool wasInMenu = false;
        private bool forceClose = false;
        private FsmBool playerInMenu;

        public override void ModSetup()
        {
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.Update, Mod_Update);
        }

        private void Mod_OnLoad()
        {
            globals = PlayMakerGlobals.Instance;
            playerInMenu = globals.Variables.FindVariable("PlayerInMenu") as FsmBool;
        }

        private void Mod_Update()
        {
            if (forceClose)
            {
                forceClose = false;
                if (optionsMenu == null)
                    optionsMenu = GameObject.Find("Systems/OptionsMenu");

                if (playerInMenu != null) playerInMenu.Value = false;
                if (optionsMenu != null) optionsMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && wasInMenu)
            {
                TryCloseMenu();
                forceClose = true;
            }

            wasInMenu = playerInMenu != null && playerInMenu.Value;
        }

        private void TryCloseMenu()
        {
            if (playerInMenu != null) playerInMenu.Value = false;

            if (optionsMenu == null)
                optionsMenu = GameObject.Find("Systems/OptionsMenu");

            if (optionsMenu != null) optionsMenu.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}