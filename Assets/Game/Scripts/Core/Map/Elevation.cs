using UnityEngine;
using System.Collections.Generic;

namespace MS
{
	public class Elevation : MonoBehaviour
	{
	    public GameObject   ElevationPrefab;
        public GameObject   DepresionPrefab;
        public Transform    Container;
        public Transform    Terrain;
	    public float        VerticalOffset;

        protected int       m_CurrentHeight = 0;

	    protected GameObject[] m_InstantiatedElevations;

        #region Properties

        public int Height
        {
            get
            {
                return m_CurrentHeight;
            }
        }

        #endregion

	    public void ChangeHeight(int newHeight)
		{
            if (m_InstantiatedElevations == null)
            {
                m_InstantiatedElevations = new GameObject[7];
            }
            
            int steps;
            
            steps = Mathf.Abs(newHeight - m_CurrentHeight);
            
			if (newHeight > m_CurrentHeight)
            {
                for (int i = 0; i < steps; i++)
                {
                    LevelUp();                    
                }
            }
            else if (newHeight < m_CurrentHeight)
            {
                for (int i = 0; i < steps; i++)
                {
                    LevelDown();                    
                }
            }
		}
        
        public void UpdateVisibleElevations(int lowestHeightAmongNeighbors)
        {
            for (int i = -3; i < lowestHeightAmongNeighbors; i++)
            {
                RemoveElevation(i);
            }
            
            FillElevations(m_CurrentHeight, lowestHeightAmongNeighbors);
        }

        protected void LevelUp()
        {
            m_CurrentHeight++;
            
            AddElevation(m_CurrentHeight -1 );
            MoveGround(m_CurrentHeight);
        }

        protected void LevelDown()
        {
            RemoveElevation(m_CurrentHeight - 1);
            MoveGround(m_CurrentHeight - 1);
            
            m_CurrentHeight--;
        }

        /// <summary>
        /// Adds a new elevation or depresion mesh below or above the height level.
        /// </summary>
        /// <param name="height">Height.</param>
        protected void AddElevation(int height)
        {
            int index;
            
            index = HeightToIndex(height);
            
            if (m_InstantiatedElevations[index] == null)
            {
                GameObject  prefabToInstantiate;
                GameObject  instantiatedObj;
    
                prefabToInstantiate     =   height >= 0 ? ElevationPrefab : DepresionPrefab;
                instantiatedObj         =   Instantiate(prefabToInstantiate.gameObject, this.transform.position, this.transform.rotation) as GameObject;
    
                instantiatedObj.transform.SetParent(Container);
    
                instantiatedObj.transform.position = this.transform.position + (this.transform.up * height * VerticalOffset);
    
                m_InstantiatedElevations[index] = instantiatedObj;
            }
        }
        
        protected void RemoveElevation(int height)
        {
            int index;
            
            index = HeightToIndex(height);
            
            if (m_InstantiatedElevations[index] != null)
            {
                Destroy(m_InstantiatedElevations[index]);
                m_InstantiatedElevations[index] = null;
            }
        }
        
        public void FillElevations(int topHeight, int bottomHeight)
        {
            for (int i = bottomHeight; i < topHeight; i++)
            {
                AddElevation(i);
            }
        }
        
        public void FillFromCurrentTo(int bottom)
        {
            FillElevations(m_CurrentHeight, bottom);
        }

        protected void MoveGround(int height)
        {
            Terrain.position = this.transform.position + (this.transform.up * height * VerticalOffset);
        }
        
        /// <summary>
        /// Converts from a certain height to the index where that height elements are stored in the internal List.
        /// </summary>
        /// <param name="height">Height, from -3 to 3, that we want to get the index.</param>
        /// <return>Index of the internal list where the elements of the height specified are stored.</return>
        protected int HeightToIndex(int height)
        {
            if (height < -3 || height > 3)
            {
                throw new System.IndexOutOfRangeException("Passed Height " + height);
            }
            return height + 3;
        }
	}
}
