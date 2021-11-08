using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    #region directions
    private Direction NORTH = new Direction(1, 0);
    private Direction SOUTH = new Direction(-1, 0);
    private Direction EAST = new Direction(0, 1);
    private Direction WEAST = new Direction(0, -1);
    private Direction[] directions;
    #endregion

    #region maze data structures
    private bool[,] nodesVisited;
    private bool[] wallHorizontalDestroyed;
    private bool[] wallVerticalDestroyed;
    Index2D startingPosition;
    Index2D goalPosition;
    List<Index2D> remainingNodes;
    #endregion

    private int numNodesVisited;

    public MazeGenerator()
    {
        directions = new Direction[4];
        directions[0] = NORTH;
        directions[1] = SOUTH;
        directions[2] = EAST;
        directions[3] = WEAST;
    }
    public void Init(int height, int width)
    {
        nodesVisited = new bool[height, width];
        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                nodesVisited[i, j] = false;

        wallHorizontalDestroyed = new bool[(height - 1) * width];
        for (int i = 0; i < (height - 1) * width; i++)
        {
            wallHorizontalDestroyed[i] = false;
        }

        wallVerticalDestroyed = new bool[height * (width - 1)];
        for (int i = 0; i < height * (width - 1); i++)
        {
            wallVerticalDestroyed[i] = false;
        }

        remainingNodes = new List<Index2D>();
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                remainingNodes.Add(new Index2D(i, j));
            }
        }
    }
    public void Generate()
    {
        numNodesVisited = 0;
        Index2D index = new Index2D();
        index.Randomize(nodesVisited.GetLength(0), nodesVisited.GetLength(1));
        Visit(index);
        int numNodes = nodesVisited.GetLength(0) * nodesVisited.GetLength(1);
        while (numNodesVisited < numNodes)
        {
            RandomWalk();
        }
        StartingAndGoalPositions();

        void RandomWalk()
        {
            Index2D initPos = InitialVertexNew();
            Index2D finalPos;
            List<Direction> walk;
            Walk(initPos, out finalPos, out walk);
            Backtrack(finalPos, walk);

            Index2D InitialVertexNew()
            {
                while (remainingNodes.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, remainingNodes.Count);
                    Index2D position = remainingNodes[index];
                    if (nodesVisited[position.row, position.col])
                    {
                        remainingNodes.RemoveAt(index);
                    }
                    else
                    {
                        return position;
                    }
                }
                return null;
            }
            void Walk(Index2D initialPos, out Index2D finalPos, out List<Direction> walk)
            {
                List<Index2D> nodesVisitedInWalk = new List<Index2D>();
                Index2D.Add(initialPos, nodesVisitedInWalk);
                finalPos = initialPos;
                walk = new List<Direction>();
                do
                {
                    Direction direction;
                    do
                    {
                        direction = directions[UnityEngine.Random.Range(0, directions.Length)];
                    } while (!ValidDirection(direction, finalPos, walk));

                    finalPos += direction;
                    if (AlreadyVisitedInWalk(finalPos, nodesVisitedInWalk))
                    {
                        nodesVisitedInWalk.Clear();
                        walk.Clear();
                    }
                    else
                    {
                        Direction.Add(direction, walk);
                    }
                    Index2D.Add(finalPos, nodesVisitedInWalk);
                } while (!nodesVisited[finalPos.row, finalPos.col]);
                bool ValidDirection(Direction direction, Index2D position, List<Direction> previousDirections)
                {
                    if (previousDirections.Count > 0 && direction.Oposite(previousDirections[previousDirections.Count - 1]))
                    {
                        return false;
                    }
                    Index2D finalPos = position + direction;
                    if (finalPos.row < 0 || finalPos.row >= nodesVisited.GetLength(0) ||
                        finalPos.col < 0 || finalPos.col >= nodesVisited.GetLength(1))
                    {
                        return false;
                    }
                    return true;
                }
                bool AlreadyVisitedInWalk(Index2D position, List<Index2D> nodesVisitedInWalk)
                {
                    return nodesVisitedInWalk.Contains(position);
                }
            }
            void Backtrack(Index2D position, List<Direction> walk)
            {
                int iterator = walk.Count - 1;
                while (iterator >= 0)
                {
                    Index2D newPosition = position - walk[iterator];
                    Visit(newPosition);
                    ChangeWall(newPosition, position, true);
                    position = newPosition;
                    iterator--;
                }
            }
        }
        void StartingAndGoalPositions()
        {
            startingPosition = new Index2D(0, 0);
            goalPosition = FarthestPosition(startingPosition);
            for(int i = 0; i < nodesVisited.GetLength(0); i++)
            {
                Index2D newStartingPosition = FarthestPosition(goalPosition);
                if (newStartingPosition == startingPosition)
                    break;
                else
                    startingPosition = newStartingPosition;

                Index2D newGoalPosition = FarthestPosition(startingPosition);
                if (newGoalPosition == goalPosition)
                    break;
                else
                    goalPosition = newGoalPosition;
            }
        }
        List<Direction> PosibleDirections(Index2D currentPosition, Direction previousDirection)
        {
            List<Direction> posibleDirections = new List<Direction>();
            foreach (Direction direction in directions)
            {
                if (ValidDirection(direction, previousDirection, currentPosition))
                    Direction.Add(direction, posibleDirections);
            }
            return posibleDirections;

            bool ValidDirection(Direction direction, Direction previousDirection, Index2D currentPosition)
            {
                if (direction.Oposite(previousDirection))
                    return false;
                else
                {
                    Index2D nextPosition = currentPosition + direction;
                    if (!InsideMaze(nextPosition))
                        return false;
                    else
                        return !HasWall(currentPosition, nextPosition);
                }
                bool InsideMaze(Index2D position)
                {
                    return position.row >= 0 && position.col >= 0 && position.row < nodesVisited.GetLength(0) &&
                        position.col < nodesVisited.GetLength(1);
                }
            }
        }
        Index2D FarthestPosition(Index2D position)
        {
            List<IndexDistance> distances = new List<IndexDistance>();
            Recursive(position, new Direction(0, 0), 0);
            return ChooseFarthest(distances);

            void Recursive(Index2D currentPosition, Direction previousDirection, int distance)
            {
                List<Direction> posibleDirections = PosibleDirections(currentPosition, previousDirection);
                if (posibleDirections.Count == 0)
                    distances.Add(new IndexDistance(currentPosition, distance));
                else
                {
                    foreach (Direction direction in posibleDirections)
                    {
                        Index2D nextPosition = currentPosition + direction;
                        Recursive(nextPosition, direction, distance + 1);
                    }
                }
            }
            Index2D ChooseFarthest(List<IndexDistance> distances)
            {
                if(distances.Count > 0)
                {
                    Index2D goalPosition = startingPosition;
                    int maxDistance = 0;
                    foreach(IndexDistance distance in distances)
                    {
                        if(distance.distance > maxDistance)
                        {
                            maxDistance = distance.distance;
                            goalPosition = distance.index;
                        }
                    }
                    return goalPosition;
                }
                return null;
            }
        }
    }
    
    #region modify maze
    private void Visit(Index2D position)
    {
        nodesVisited[position.row, position.col] = true;
        numNodesVisited++;
    }
    private void ChangeWall(Index2D position1, Index2D position2, bool destroy)
    {
        if (position1.row == position2.row)
        {
            int index = HashFuntionVertical(position1.row, Math.Min(position1.col, position2.col));
            wallVerticalDestroyed[index] = destroy;
        }
        else if (position1.col == position2.col)
        {
            int index = HashFuntionHorizontal(Math.Min(position1.row, position2.row), position1.col);
            wallHorizontalDestroyed[index] = destroy;
        }
        else
        {
            Debug.Log("Not row or col equal");
        }
    }
    #endregion
    #region consult
    public Index2D GetStartingPosition()
    {
        return startingPosition;
    }
    public Index2D GetGoalPosition()
    {
        return goalPosition;
    }
    public bool GetBottomWall(int row, int col)
    {
        return wallHorizontalDestroyed[HashFuntionHorizontal(row, col)];
    }
    public bool GetRightWall(int row, int col)
    {
        return wallVerticalDestroyed[HashFuntionVertical(row, col)];
    }
    private bool HasWall(Index2D position1, Index2D position2)
    {
        if (position1.row == position2.row)
        {
            int index = HashFuntionVertical(position1.row, Math.Min(position1.col, position2.col));
            return !wallVerticalDestroyed[index];
        }
        else if (position1.col == position2.col)
        {
            int index = HashFuntionHorizontal(Math.Min(position1.row, position2.row), position1.col);
            return !wallHorizontalDestroyed[index];
        }
        else
        {
            Debug.Log("Not row or col equal");
            return false;
        }
    }
    public static void MazeDimensions(DIFICULTIES dificulty, out int height, out int width)
    {
        if (dificulty == DIFICULTIES.VERY_EASY)
        {
            height = 4;
            width = 10;
            return;
        }
        if (dificulty == DIFICULTIES.EASY)
        {
            height = 6;
            width = 15;
            return;
        }
        if (dificulty == DIFICULTIES.MEDIUM)
        {
            height = 12;
            width = 30;
            return;
        }
        if(dificulty == DIFICULTIES.HARD)
        {
            height = 18;
            width = 45;
            return;
        }
        if(dificulty == DIFICULTIES.VERY_HARD)
        {
            height = 24;
            width = 60;
            return;
        }
        height = 12;
        width = 30;
    }
    #endregion
    #region hash functions
    private int HashFuntionVertical(int row, int col)
    {
        return col * nodesVisited.GetLength(0) + row;
    }
    private int HashFuntionHorizontal(int row, int col)
    {
        return row * nodesVisited.GetLength(1) + col;
    }
    #endregion
}