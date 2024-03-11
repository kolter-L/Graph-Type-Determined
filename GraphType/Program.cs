// See https://aka.ms/new-console-template for more information


using System.Runtime.CompilerServices;

// hard-coded input matrix

int[,] loopMatrix = { { 0, 1, 0, 0, 0, 1 },  
                      { 1, 0, 1, 0, 0, 0 },   
                      { 0, 1, 0, 1, 0, 0 },   
                      { 0, 0, 1, 0, 1, 0 },
                      { 0, 0, 0, 1, 0, 1 },
                      { 1, 0, 0, 0, 1, 0 }};


int[,] brokenLoopMatrix = { { 0, 1, 1, 0, 0, 0 },
                            { 1, 0, 1, 0, 0, 0 },
                            { 1, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, 1 },
                            { 0, 0, 0, 1, 0, 1 },
                            { 0, 0, 0, 1, 1, 0 }};

int[,] tinyMesh = { { 0, 1, 1, 1 },
                    { 1, 0, 1, 1 },
                    { 1, 1, 0, 1 },
                    { 1, 1, 1, 0 }};

int[,] starMatrix = { { 0, 0, 0, 1 },
                      { 0, 0, 0, 1 },
                      { 0, 0, 0, 1 },
                      { 1, 1, 1, 0 }};



// function to determine the number of connections for each node
int[] graphType(int[,] matrix)
{
    int[] numConnections = new int[matrix.GetLength(0)];

    // we return this array as an error code if the matrix 
    int[] errorCode = { -1 };

    for (int i = 0; i < matrix.GetLength(0); i++)
    {

        //check if the matrix is valid
        if (matrix[i, i] != 0)
        {
            return errorCode;
        }

        for (int j = 0; j < matrix.GetLength(0); j++)
        {

            // increment the index if the entry is 1
            if (matrix[i, j] != 0)
            {
                ++numConnections[i];

            }

            // check if matrix is symmetric
            if (matrix[i, j] != matrix[j, i])
            {
                return errorCode;
            }
        }
    }
    return numConnections;
}

// DFS algorithm to check if the loops are connected: 

string IsConected(int[] myGraph, int[,] matrix)
{
    string connected = "";

    {
        int[] nodeVisited = new int[myGraph.Length];

        // begin at the first node and mark it as visited
        nodeVisited[0] = 1;

        // index to keep track of current node
        int currentNode = 0;

        // total number of nodes visited
        int totalVisited = 1;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                if (matrix[currentNode, j] == 1 && nodeVisited[j] == 0)
                {
                    // reset current node
                    currentNode = j;
                    // mark new node as visited
                    nodeVisited[currentNode] = 1;
                    // increment the total visited
                    totalVisited++;

                    break;
                }
            }
        }


        if (totalVisited != myGraph.Length) { connected = "But It is Not a Single, Connected Loop"; }
    }

    return connected;
}


string TypeDetermined(int[] myGraph)
{
    string graphType = "";

    // booleans for each graph type
    bool isLoop = true;
    bool isStar = false;
    bool isMesh = true;

    // counter to keep track of number of singly connected nodes
    int numSingles = 0;
    int possibleCenter = 0;

    for (int i = 0; i < myGraph.Length; i++)
    {
        if (myGraph[i] == -1) { return "invalid input"; }

        if (myGraph[i] != 2) { isLoop = false; }

        if (myGraph[i] != myGraph.Length - 1) { isMesh = false; }

        if (myGraph[i] == 1) { ++numSingles; }

        if (myGraph[i] >= possibleCenter) { possibleCenter = myGraph[i]; }

    }

    if (numSingles == myGraph.Length - 1 && possibleCenter == myGraph.Length - 1) { isStar = true; }

    if (isStar) { graphType = "Star"; }
    else if (isLoop) { graphType = "Loop"; }
    else if (isMesh) { graphType = "Mesh";  }
    else { graphType = "garbage"; }
    

    return graphType;
}


// array of connections at each node
int[] myGraph = graphType(brokenLoopMatrix);

// graph type determined based upon the array
string graphOut = TypeDetermined(myGraph);

string isConnected = "";

// if the graph is a loop, we check whether it is connected
if (graphOut == "Loop")
{
    isConnected = IsConected(myGraph, brokenLoopMatrix); // make sure to change the matrix here, too
}

Console.WriteLine( "This graph is a " + graphOut + ". " + isConnected);



