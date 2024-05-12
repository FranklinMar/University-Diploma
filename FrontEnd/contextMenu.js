var Menu = document.getElementById("context-menu");
var MenuState = 0;
//var ContextMenuActive = "block";

// Turns the custom context Menu on.
function toggleMenuOn() {
    if (MenuState !== 1) {
        MenuState = 1;
        //Menu.classList.add(ContextMenuActive);
        Menu.style.visibility = "visible";
    }
}
// Turns the custom context Menu off.
function toggleMenuOff() {
    if (MenuState !== 0) {
        MenuState = 0;
        //Menu.classList.remove(ContextMenuActive);
        Menu.style.visibility = "hidden";
    }
}
// Get the position of the right-click in window and returns the X and Y coordinates
function getPosition(event) {
    var PosX = 0;
    var PosY = 0;

    if (!event) var e = window.event;

    if (event.pageX || event.pageY) {
        PosX = event.pageX;
        PosY = event.pageY;
    } else if (event.clientX || event.clientY) {
        PosX =
            event.clientX +
            document.body.scrollLeft +
            document.documentElement.scrollLeft;
        PosY =
            event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }

    return {
        x: PosX,
        y: PosY
    };
}

// Position the Context Menu in right position.
function positionMenu(event) {
    let clickCoords = getPosition(event);
    let clickCoordsX = clickCoords.x;
    let clickCoordsY = clickCoords.y;

    let MenuWidth = Menu.offsetWidth + 4;
    let MenuHeight = Menu.offsetHeight + 4;

    let windowWidth = window.innerWidth;
    let windowHeight = window.innerHeight;

    if (windowWidth - clickCoordsX < MenuWidth) {
        Menu.style.left = windowWidth - MenuWidth + "px";
    } else {
        Menu.style.left = clickCoordsX + "px";
    }

    if (windowHeight - clickCoordsY < MenuHeight) {
        Menu.style.top = windowHeight - MenuHeight + "px";
    } else {
        Menu.style.top = clickCoordsY + "px";
    }
}