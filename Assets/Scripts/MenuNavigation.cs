using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public enum MenuState {mainMenu, optionsMenu };
    public MenuState currentMenuState = MenuState.mainMenu;

    public void ChangeState(MenuState nextState) //Switches the menu state to the state that is currently open
    {
        currentMenuState = nextState;
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnPlayButtonClicked()
    {
        // Loads the scene the game is in
        SceneManager.LoadScene(1);
    }

    public void OnOptionsButtonClicked()
    {
        optionsMenu.SetActive(true); //Turns options menu on
        mainMenu.SetActive(false); // turns the main menu off
        ChangeState(MenuState.optionsMenu); //changes the menu state to options menu
    }

    public void OnMainMenuButtonClicked()
    {
        optionsMenu.SetActive(false); // Turns option menu off
        mainMenu.SetActive(true); // turns on main menu
        ChangeState(MenuState.mainMenu); // changes menu state to main menu
    }
}
