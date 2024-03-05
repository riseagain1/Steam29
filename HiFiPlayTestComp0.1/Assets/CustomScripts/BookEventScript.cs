using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookEventScript : MonoBehaviour
{
    public GameObject book;
    public GameObject bookLeftPanel;
    private HingeJoint bookLeftHingeJoint;

    // right page
    public GameObject pageRight;
    private Collider pageRightCollider;
    private MeshRenderer pageRightMesh;
    private HingeJoint pageRightHingeJoint;
    private JointLimits pageRightHingeJointLimits;

    public GameObject pageRightUpperSpriteHolder;
    private SpriteRenderer pageRightUpperSprite;

    public GameObject pageRightLowerSpriteHolder;
    private SpriteRenderer pageRightLowerSprite;

    // right under page
    public GameObject pageRightUnder;
    public GameObject pageRightUnderSpriteHolder;
    private SpriteRenderer pageRightUnderSprite;

    // left page
    public GameObject pageLeft;
    private Collider pageLeftCollider;
    private MeshRenderer pageLeftMesh;
    private HingeJoint pageLeftHingeJoint;
    private JointLimits pageLeftHingeJointLimits;
    
    public GameObject pageLeftUpperSpriteHolder;
    private SpriteRenderer pageLeftUpperSprite;

    public GameObject pageLeftLowerSpriteHolder;
    private SpriteRenderer pageLeftLowerSprite;

    // left under page
    public GameObject pageLeftUnder;
    public GameObject pageLeftUnderSpriteHolder;
    private SpriteRenderer pageLeftUnderSprite;

    // list of page sprites
    public List<Sprite> spriteList = new List<Sprite>();
    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        // book cover
        bookLeftHingeJoint = bookLeftPanel.GetComponent<HingeJoint>();


        // right page
        pageRightCollider = pageRight.GetComponent<Collider>();
        pageRightMesh = pageRight.GetComponent<MeshRenderer>();
        pageRightHingeJoint = pageRight.GetComponent<HingeJoint>();
        pageRightHingeJointLimits.min = 0;
        pageRightHingeJointLimits.max = 175;

        pageRightUpperSprite = pageRightUpperSpriteHolder.GetComponent<SpriteRenderer>();
        pageRightLowerSprite = pageRightLowerSpriteHolder.GetComponent<SpriteRenderer>();
        pageRightUnderSprite = pageRightUnderSpriteHolder.GetComponent<SpriteRenderer>();

        // left page
        pageLeftCollider = pageLeft.GetComponent<Collider>();
        pageLeftMesh = pageLeft.GetComponent<MeshRenderer>();
        pageLeftHingeJoint = pageLeft.GetComponent<HingeJoint>();
        pageLeftHingeJointLimits.min = -175;
        pageLeftHingeJointLimits.max = -2;

        pageLeftUpperSprite = pageLeftUpperSpriteHolder.GetComponent<SpriteRenderer>();
        pageLeftLowerSprite = pageLeftLowerSpriteHolder.GetComponent<SpriteRenderer>();
        pageLeftUnderSprite = pageLeftUnderSpriteHolder.GetComponent<SpriteRenderer>();

        // setup pages
        currentPage = -1;
        changePages();
    }

    // Update is called once per physics frame
    void Update()
    {  
        
        //Debug.Log("Book Left Panel angle" + bookLeftPanel.transform.localEulerAngles.z);
        if(bookLeftHingeJoint.angle < -170){
            //Debug.Log("book closed");
            pageRightCollider.enabled = false;
            pageLeftCollider.enabled = false;
            // setup pages
            currentPage = -1;
            changePages();
            
        } else {

            // page right
            if(!pageRightCollider.enabled){
                pageRightCollider.enabled = true;
            }
            
            pageRightHingeJoint.limits = pageRightHingeJointLimits;
            
            if(pageRightHingeJoint.angle > (174 + bookLeftHingeJoint.angle) 
            && pageRightMesh.enabled){
                // turned right page to leftside(show next pages)
                Debug.Log("Right page turned to left");
                
                pageRightHingeJointLimits.max = 1; // move page back
                pageRightMesh.enabled = false;
                pageRightUpperSprite.enabled = false;
                pageRightLowerSprite.enabled = false;
                StartCoroutine(ResetRightLimitAfterDelay());
            }

            

            // page left
            if(!pageLeftCollider.enabled){
                pageLeftCollider.enabled = true;
            }

            pageLeftHingeJointLimits.max = bookLeftHingeJoint.angle - 1;
            pageLeftHingeJoint.limits = pageLeftHingeJointLimits;

            if(pageLeftHingeJoint.angle < -170 
            && pageLeftMesh.enabled){
                // turned left page to right side(show previous pages)
                Debug.Log("Left page turned to right");

                pageLeftHingeJointLimits.min = -3 + bookLeftHingeJoint.angle; // move page back
                pageLeftMesh.enabled = false;
                pageLeftUpperSprite.enabled = false;
                pageLeftLowerSprite.enabled = false;
                StartCoroutine(ResetLeftLimitAfterDelay());
            }


            
        }
        //Debug.Log("AngleBook::" + bookLeftHingeJoint.angle);
        //Debug.Log("AnglePageR:" + pageRightHingeJoint.angle);
        
    }

    IEnumerator ResetRightLimitAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        pageRightHingeJointLimits.max = 175; 

        currentPage+=2;
        changePages();
    }

    IEnumerator ResetLeftLimitAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        pageLeftHingeJointLimits.min = -175;

        currentPage-=2;
        changePages();
    }

    public void BookLeftPanelGrabbed(){
        Debug.Log("BookLeftPanel Grabbed");
        
    }

    public void changePages(){
        Debug.Log("Current left upper page:" + currentPage);

        if(currentPage == -1){ // at start of book(initialize some things)
            // no left page
            pageLeftMesh.enabled = false;
            pageLeftUpperSprite.enabled = false;
            pageLeftLowerSprite.enabled = false;
            pageLeftUnder.SetActive(false);

            pageRightMesh.enabled = true;
            pageRightUpperSprite.enabled = true;
            pageRightLowerSprite.enabled = true;
            pageRightUnder.SetActive(true);
            pageRightUnderSprite.enabled = true;
            pageRightUpperSprite.sprite = spriteList[0];
            pageRightLowerSprite.sprite = spriteList[1];
            pageRightUnderSprite.sprite = spriteList[2];
        }

        if(currentPage >= 1){ // past start, now have left page

            // ------LEFT SIDE-------

            // LEFT UPPER
            if(spriteList.Count <= (currentPage)) { // no page found
                pageLeftUpperSprite.enabled = false;
            } else {
                pageLeftMesh.enabled = true;
                pageLeftUpperSprite.enabled = true;
                pageLeftUpperSprite.sprite = spriteList[currentPage];
            }

            // LEFT LOWER
            if((currentPage - 1) < 0) { // no page found
                //pageLeftLowerSprite = false;
            } else {
                pageLeftLowerSprite.enabled = true;
                pageLeftLowerSprite.sprite = spriteList[currentPage - 1];
            }

            // LEFT UNDER
            if((currentPage - 2) < 0) { // no page found
                pageLeftUnder.SetActive(false);
                //pageLeftUnderSprite.enabled = false;
            } else {
                pageLeftUnder.SetActive(true);
                pageLeftUnderSprite.enabled = true;
                pageLeftUnderSprite.sprite = spriteList[currentPage - 2];
            }

            
            
            // ------RIGHT SIDE-------

            // RIGHT UPPER
            Debug.Log("spritelist length: " + spriteList.Count + " currentpage + 1: " + (currentPage + 1));
            if(spriteList.Count <= (currentPage + 1)) { // no page found
                //pageRightMesh.enabled = false;
                //pageRightUpperSprite.enabled = false;
                Debug.Log("right disabled");
            } else {
                pageRightMesh.enabled = true;
                pageRightUpperSprite.enabled = true;
                pageRightUpperSprite.sprite = spriteList[currentPage + 1];
            }

            // RIGHT LOWER
            if(spriteList.Count <= (currentPage + 2)) { // no page found
                //pageRightLowerSprite.enabled = false;
            } else { // page found
                pageRightLowerSprite.enabled = true;
                pageRightLowerSprite.sprite = spriteList[currentPage + 2];
            }


            // RIGHT UNDER
            if(spriteList.Count <= (currentPage + 3)) { // no page found
                //pageRightUnderSprite.enabled = false;
                pageRightUnder.SetActive(false);
            } else { // page found
                pageRightUnder.SetActive(true);
                pageRightUnderSprite.enabled = true;
                pageRightUnderSprite.sprite = spriteList[currentPage + 3];
            }
            
            
        }
    }
}
