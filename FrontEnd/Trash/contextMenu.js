/*< div id = "context-menu" >
    <div class="item" id="addNode">Add Node</div>
    <div class="item" id="deleteNode" style="display: none">Delete Node</div>
    <div class="item" id="addEdge" style="display: none">Add Edge</div>
    <div class="item" id="deleteEdge" style="display: none">Delete Edge</div>
</div >*/

//var Menu = document.getElementById("context-menu");
//var MenuState = true;
DivCy.addEventListener('contextmenu', function (event) {
    event.preventDefault();
    toggleMenuOn();
    positionMenu(event);
});
/*Cy.on('contextmenu', "none", function (event) {
    event.preventDefault();
    toggleMenuOn();
    positionMenu(event);
})*/

// Event Listener for Close Context Menu when outside of menu clicked
document.addEventListener("click", (event) => {
    let Button = event.which || event.button;
    if (Button === 1) {
        toggleMenuOff();
    }
});

// Close Context Menu on Esc key press
window.onkeyup = function (event) {
    if (event.keyCode === 27) {
        toggleMenuOff();
    }
}

// Turns the custom context Menu on.
function toggleMenuOn() {
    if (MenuState !== true) {
        MenuState = true;
        Menu.style.visibility = "visible";
    }
}
// Turns the custom context Menu off.
function toggleMenuOff() {
    if (MenuState !== false) {
        MenuState = false;
        Menu.style.visibility = "hidden";
    }
}
// Get the position of the right-click in window and returns the X and Y coordinates
function getPosition(event) {
    let posx = 0;
    let posy = 0;

    if (!event) var e = window.event;

    if (event.pageX || event.pageY) {
        posx = event.pageX;
        posy = event.pageY;
    } else if (event.clientX || event.clientY) {
        posx = event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        posy = event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }

    return {
        x: posx,
        y: posy
    };
}

// Position the Context Menu in right position.
function positionMenu(event) {
    let clickCoords = getPosition(event);
    //let clickCoordsX = clickCoords.x;
    //let clickCoordsY = clickCoords.y;

    let WindowWidth = window.innerWidth;
    let WindowHeight = window.innerHeight;

    let MenuWidth = Menu.offsetWidth + 4;
    let MenuHeight = Menu.offsetHeight + 4;

    if (WindowWidth - clickCoords.x < MenuWidth) {
        Menu.style.left = WindowWidth - MenuWidth + "px";
    } else {
        Menu.style.left = clickCoords.x + "px";
    }

    if (WindowHeight - clickCoords.y < MenuHeight) {
        Menu.style.top = WindowHeight - MenuHeight + "px";
    } else {
        Menu.style.top = clickCoords.y + "px";
    }
}

let addNode = document.getElementById("addNode");
addNode.addEventListener('click', function (event) {
    let WindowWidth = window.innerWidth;
    let WindowHeight = window.innerHeight;

    //let Name = 'new';
    let Position = getPosition(event);
    let Width = 50;
    let Height = 20;
    Cy.add({
        group: 'nodes',
        data: { id: ID },
        renderedPosition: Position
    });
    let NameInput = document.createElement("input");
    NameInput.type = "text";
    NameInput.style.width = Width + "px";
    NameInput.style.height = Height + "px";
    //NameInput.value = Name;
    NameInput.DataNode = ID;
    NameInput.style.position = "absolute";

    if (WindowWidth - Position.x < Width) {
        NameInput.style.left = (WindowWidth - NameInput.offsetWidth - Width / 2) + "px";
    } else {
        NameInput.style.left = (Position.x - Width / 2) + "px";
    }
    if (WindowHeight - Position.y < Height) {
        NameInput.style.top = (WindowHeight - NameInput.offsetHeight - Height * 1.5) + "px";
    } else {
        NameInput.style.top = (Position.y - Height * 1.5) + "px";
    }
    //NameInput.style.left = Position.x + "px";
    //NameInput.style.top = Position.y - 10 + "px";
    //NameInput.
    NameInput.style.zIndex = 10;
    Body.appendChild(NameInput);
    NameInput.focus();
    /*NameInput.onblur(function (event) {
        NameInput.remove();
    })*/
    NameInput.addEventListener('focusout', function (event) {
        if (/*NameInput.value === Name || */NameInput.value === "") {
            Cy.remove(`#${ID}`);
        } else {
            //Cy.getElementById(ID).data('position', { x: 0, y: 0 });
            Cy.getElementById(ID).attr('label', NameInput.value);
            ID++;
        }
        //Cy
        NameInput.remove();
    });
});
let deleteNode = document.getElementById("deleteNode");
deleteNode.addEventListener('click', function (event) {
    deleteNode.style.display = "none";
    Cy.remove(`#${deleteNode.node}`);
});
let addEdge = document.getElementById("addEdge");
let deleteEdge = document.getElementById("deleteEdge");

Cy.on('mouseover', 'node', function (event) {
    //console.log(event.target.data());
    if (!event.target.data('immutable')/* && MenuState === false*/) {
        deleteNode.style.display = "revert";
        deleteNode.node = event.target.data('id');
        addEdge.style.display = "revert";
    } else {
        deleteNode.style.display = "none";
    }
});

Cy.on('mouseoff', 'node', function (event) {
    deleteNode.style.display = "none";
});

Cy.on('unfocus', 'node', function (event) {
    deleteNode.style.display = "none";
});