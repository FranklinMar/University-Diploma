
//import cytoscape from "./FrontEnd/cytoscape.esm.min.js";

var Cy = cytoscape({

    container: DivCy, // Container to render in

    elements: [ // List of graph elements to start with
        { // Node A
            data: { id: 'a' }
        },
        { // Node B
            data: { id: 'b' }
        },
        { // Edge AB
            data: { id: 'ab', source: 'a', target: 'b' }
        }
    ],
    style: [ // The stylesheet for the graph
        {
            selector: 'node',
            style: {
                'background-color': '#666',
                'label': 'data(id)'
            }
        },
        {
            selector: 'edge',
            style: {
                'width': 3,
                'line-color': '#ccc',
                'target-arrow-color': '#ccc',
                'target-arrow-shape': 'triangle',
                'curve-style': 'bezier'
            }
        }
    ],
    /*layout: {
        name: 'grid',
        rows: 1
    }*/
});

var addNode = document.getElementById("addNode");
var deleteNode = document.getElementById("deleteNode");
var addEdge = document.getElementById("addEdge");
var deleteEdge = document.getElementById("deleteEdge");

Cy.on('mouseover', 'node', function (event) {
    //Cy.
        //Cy.elements()
    var Name = 'new';
    var Position = getPosition(event);
    var Width = 100;
    var Height = 30;
    Cy.add({
        group: 'nodes',
        data: { id: Name },
        position: Position
    })
    var NameInput = document.createElement("input");
    NameInput.type = "text";
    NameInput.width = Width + "px";
    NameInput.height = Height + "px";

    /*let MenuWidth = Menu.offsetWidth + 4;
    let MenuHeight = Menu.offsetHeight + 4;

    let windowWidth = window.innerWidth;
    let windowHeight = window.innerHeight;

    if (windowWidth - Position.x < MenuWidth) {
        Menu.style.left = windowWidth - MenuWidth + "px";
    } else {
        Menu.style.left = clickCoordsX + "px";
    }

    if (windowHeight - clickCoordsY < MenuHeight) {
        Menu.style.top = windowHeight - MenuHeight + "px";
    } else {
        Menu.style.top = clickCoordsY + "px";
    }*/
    NameInput.style.top = Position.x;
    NameInput.style.left = Position.y;
    container.appendChild(NameInput);
});