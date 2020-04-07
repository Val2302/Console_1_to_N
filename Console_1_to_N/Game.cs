using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Console_1_to_N
{
    public class Game
    {
        private const int CANDIDATS_COUNT = 8;

        private int[,] _condition;

        private int ConditionRowCount => _condition.GetLength( 0 );
        private int ConditionColCount => _condition.GetLength( 1 );
        private int CandidatsRowCount => ConditionRowCount - 2;
        private int CandidatsColCount => ConditionColCount - 2;
       
        public Game( int[,] condition )
        {
            _condition = condition;
        }

        public Answer Run()
        {
            Cell startCell = null; // startCell find in CreateCandidatsSpace()
            var solution = new int[ CandidatsRowCount, CandidatsColCount ]; // solution filled in FindPath()
            return RunGame();

            Answer RunGame()
            {
                var candidatsSpace = CreateCandidatsSpace();
                var path = FindPath( candidatsSpace, startCell );
                return new Answer( path, solution );
            }

            List<int>[,] CreateCandidatsSpace()
            {
                int firstRow = 0;
                int lastRow = ConditionRowCount - 1;
                int firstCol = 0;
                int lastCol = ConditionColCount - 1;

                var candidatsSpace = new List<int>[ CandidatsRowCount, CandidatsColCount ];
                var endRow = CandidatsRowCount - 1;
                var endCol = CandidatsColCount - 1;
                var endingSum = endRow + endCol;
                int i, j;

                for( i = 0; i < CandidatsRowCount; i++ )
                {
                    for( j = 0; j < CandidatsColCount; j++ )
                    {
                        candidatsSpace[ i, j ] = FindCandidats();
                    }
                }

                return candidatsSpace;

                List<int> FindCandidats()
                {
                    var candidats = new List<int>( CANDIDATS_COUNT );
                    var row = i + 1;
                    var col = j + 1;

                    if( _condition[ row, col ] > 0 )
                    {
                        candidats.Add( _condition[ row, col ] );
                        startCell = new Cell( i, j, _condition[ row, col ] );
                    }
                    else
                    {
                        var sumIndexes = i + j;

                        if( i == j )
                        {
                            candidats.Add( _condition[ firstRow, firstCol ] );
                        }

                        if( sumIndexes == endRow )
                        {
                            candidats.Add( _condition[ lastRow, firstCol ] );
                        }

                        if( sumIndexes == endCol )
                        {
                            candidats.Add( _condition[ firstRow, lastCol ] );
                        }

                        if( sumIndexes == endingSum )
                        {
                            candidats.Add( _condition[ lastRow, lastCol ] );
                        }

                        candidats.Add( _condition[ firstRow, col ] );
                        candidats.Add( _condition[ lastRow, col ] );
                        candidats.Add( _condition[ row, firstCol ] );
                        candidats.Add( _condition[ row, lastCol ] );
                    }

                    return candidats;
                }
            }

            List<Cell> FindPath( List<int>[,] candidatsSpace, Cell nextCell )
            {
                int firstRow = 0;
                int lastRow = CandidatsRowCount - 1;
                int firstCol = 0;
                int lastCol = CandidatsColCount - 1;
                var path = new List<Cell>( CandidatsRowCount * CandidatsColCount );

                do
                {
                    path.Add( nextCell );
                    solution[ nextCell.Row, nextCell.Col ] = nextCell.Value;

                    var nextCells = new List<Cell>( CANDIDATS_COUNT );
                    nextCell = FindNextCell( nextCell );
                } while( nextCell != null );

                return path;

                Cell FindNextCell( Cell selectCell )
                {
                    return FindCell();

                    Cell FindCell()
                    {
                        var row = selectCell.Row;
                        var col = selectCell.Col;
                        var nextNumber = selectCell.Value + 1;
                        
                        if( row == firstRow )
                        {
                            if( col == firstCol )
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col + 1, nextNumber ),
                                    FindValue( row + 1, col, nextNumber ),
                                    FindValue( row + 1, col + 1, nextNumber )
                                }.First( x => x != null );
                            }
                            else if( col == lastCol )
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col - 1, nextNumber ),
                                    FindValue( row + 1, col, nextNumber ),
                                    FindValue( row + 1, col - 1, nextNumber )
                                }.First( x => x != null );
                            }
                            else
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col + 1, nextNumber ),
                                    FindValue( row + 1, col, nextNumber ),
                                    FindValue( row + 1, col + 1, nextNumber ),
                                    FindValue( row, col - 1, nextNumber ),
                                    FindValue( row + 1, col - 1, nextNumber )
                                }.First( x => x != null );
                            }
                        }

                        if( row == lastRow )
                        {
                            if( col == firstCol )
                            {
                                var v = new Cell[]
                                {
                                    FindValue( row, col + 1, nextNumber ),
                                    FindValue( row - 1, col, nextNumber ),
                                    FindValue( row - 1, col + 1, nextNumber )
                                }.First( x => x != null );

                                return v;
                            }
                            else if( col == lastCol )
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col - 1, nextNumber ),
                                    FindValue( row - 1, col, nextNumber ),
                                    FindValue( row - 1, col - 1, nextNumber )
                                }.First( x => x != null );
                            }
                            else
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col + 1, nextNumber ),
                                    FindValue( row - 1, col, nextNumber ),
                                    FindValue( row - 1, col + 1, nextNumber ),
                                    FindValue( row, col - 1, nextNumber ),
                                    FindValue( row - 1, col - 1, nextNumber )
                                }.First( x => x != null );
                            }
                        }

                        if( row > 0 && row < lastRow )
                        {
                            if( col == firstCol )
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col + 1, nextNumber ),
                                    FindValue( row - 1, col, nextNumber ),
                                    FindValue( row + 1, col, nextNumber ),
                                    FindValue( row - 1, col + 1, nextNumber ),
                                    FindValue( row + 1, col + 1, nextNumber )
                                }.First( x => x != null );
                            }
                            else if( col == lastCol )
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col - 1, nextNumber ),
                                    FindValue( row - 1, col, nextNumber ),
                                    FindValue( row + 1, col, nextNumber ),
                                    FindValue( row + 1, col - 1, nextNumber ),
                                    FindValue( row - 1, col - 1, nextNumber )
                                }.First( x => x != null );
                            }
                            else
                            {
                                return new Cell[]
                                {
                                    FindValue( row, col + 1, nextNumber ),
                                    FindValue( row, col - 1, nextNumber ),
                                    FindValue( row + 1, col, nextNumber ),
                                    FindValue( row - 1, col, nextNumber ),
                                    FindValue( row + 1, col + 1, nextNumber ),
                                    FindValue( row + 1, col - 1, nextNumber ),
                                    FindValue( row - 1, col - 1, nextNumber ),
                                    FindValue( row - 1, col + 1, nextNumber )
                                }.First( x => x != null );
                            }
                        }

                        return null;
                    }
                    
                    Cell FindValue ( int row, int col, int number )
                    {   
                        return candidatsSpace[ row, col ].IndexOf( number ) > -1 ? new Cell( row, col, number ) : null;
                    }
                }
            }
        }

        public bool IsWonGame()
        {
            return true;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
