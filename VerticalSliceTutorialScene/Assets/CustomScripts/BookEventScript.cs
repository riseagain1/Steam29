using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookEventScript : MonoBehaviour
{
    public GameObject book;
    public ParticleSystem outlineSparkles;
    public GameObject bookLeftPanel;
    private HingeJoint bookLeftHingeJoint;

    // right page
    public GameObject pageRight;
    public GameObject pageRightOutline;
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
    public GameObject pageRightUnderOutline;
    public GameObject pageRightUnderSpriteHolder;
    private SpriteRenderer pageRightUnderSprite;

    // left page
    public GameObject pageLeft;
    public GameObject pageLeftOutline;
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
    public GameObject pageLeftUnderOutline;
    public GameObject pageLeftUnderSpriteHolder;
    private SpriteRenderer pageLeftUnderSprite;

    // list of page sprites
    public List<Sprite> spriteList = new List<Sprite>();
    private int currentPage;
    public int outlinePage = -1; // no page selected, no outline
    private int outlineCaseValue = -1;

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
        if (outlineCaseValue != -1){
            conditionToEndNotif(outlineCaseValue);
        }
        

        if (outlinePage != -1 ){ // new page added, enable particles
            if(!outlineSparkles.isPlaying){
                outlineSparkles.Play();
            } 
        } else { // no new pages
            pageLeftOutline.SetActive(false);
            pageLeftUnderOutline.SetActive(false);
            pageRightOutline.SetActive(false);
            pageRightUnderOutline.SetActive(false);
        }

        //Debug.Log("Right page angle: " + pageRightHingeJoint.angle + " left page angle: " + pageLeftHingeJoint.angle);
        
        //Debug.Log("Book Left Panel angle" + bookLeftHingeJoint.angle);

        if(bookClosed()){
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
            && pageRightMesh.enabled && spriteList.Count >= 3){
                // turned right page to leftside(show next pages)
                //Debug.Log("Right page turned to left");
                
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
                //Debug.Log("Left page turned to right");

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
        //Debug.Log("BookLeftPanel Grabbed");
        
    }

    public void changePages(){
        //Debug.Log("Current left upper page:" + currentPage);
        pageLeftOutline.SetActive(false);
        pageLeftUnderOutline.SetActive(false);
        pageRightOutline.SetActive(false);
        pageRightUnderOutline.SetActive(false);

        if(currentPage == -1){ // at start of book(initialize some things)
            // no left page
            pageLeftMesh.enabled = false;
            pageLeftUpperSprite.enabled = false;
            pageLeftLowerSprite.enabled = false;
            pageLeftUnder.SetActive(false);

            if (spriteList.Count >= 1){ // atleast 1 page exists
                pageRightMesh.enabled = true;
                pageRightUpperSprite.enabled = true;
                pageRightUpperSprite.sprite = spriteList[0];
                if(outlinePage == 0){
                    pageRightOutline.SetActive(true);
                    outlineCaseValue = 1; // right upper is new page CASE 1
                }

                if(spriteList.Count >= 2){ // atleast 2 pages exists
                    pageRightLowerSprite.enabled = true;
                    pageRightLowerSprite.sprite = spriteList[1];
                    
                    if(outlinePage == 1){
                        pageRightOutline.SetActive(true);
                        outlineCaseValue = 0; // right lower is new page CASE 0
                    }

                    if (spriteList.Count >= 3){ // atleast 3 pages exists
                        pageRightUnder.SetActive(true);
                        pageRightUnderSprite.enabled = true;
                        pageRightUnderSprite.sprite = spriteList[2];

                        if(outlinePage == 2){
                            pageRightUnderOutline.SetActive(true);
                        }

                    }
                }
            } else { // no right page either
                pageRightMesh.enabled = false;
                pageRightUpperSprite.enabled = false;
                pageRightLowerSprite.enabled = false;
                pageRightUnder.SetActive(false);
            }

            
            
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
                if(outlinePage == currentPage){
                    pageLeftOutline.SetActive(true);
                    outlineCaseValue = 2; // left upper is new page CASE 2
                }
            }

            // LEFT LOWER
            if((currentPage - 1) < 0) { // no page found
                //pageLeftLowerSprite = false;
            } else {
                pageLeftLowerSprite.enabled = true;
                pageLeftLowerSprite.sprite = spriteList[currentPage - 1];
                if(outlinePage == currentPage - 1){
                    pageLeftOutline.SetActive(true);
                }
            }

            // LEFT UNDER
            if((currentPage - 2) < 0) { // no page found
                pageLeftUnder.SetActive(false);
                //pageLeftUnderSprite.enabled = false;
            } else {
                pageLeftUnder.SetActive(true);
                pageLeftUnderSprite.enabled = true;
                pageLeftUnderSprite.sprite = spriteList[currentPage - 2];
                if(outlinePage == currentPage - 2){
                    pageLeftUnderOutline.SetActive(true);
                }
            }

            
            
            // ------RIGHT SIDE-------

            // RIGHT UPPER
            //Debug.Log("spritelist length: " + spriteList.Count + " currentpage + 1: " + (currentPage + 1));
            if(spriteList.Count <= (currentPage + 1)) { // no page found
                //pageRightMesh.enabled = false;
                //pageRightUpperSprite.enabled = false;
                //Debug.Log("right disabled");
            } else {
                pageRightMesh.enabled = true;
                pageRightUpperSprite.enabled = true;
                pageRightUpperSprite.sprite = spriteList[currentPage + 1];
                if(outlinePage == currentPage + 1){
                    pageRightOutline.SetActive(true);
                    outlineCaseValue = 1; // right upper is new page CASE 1
                }
            }

            // RIGHT LOWER
            if(spriteList.Count <= (currentPage + 2)) { // no page found
                //pageRightLowerSprite.enabled = false;
            } else { // page found
                pageRightLowerSprite.enabled = true;
                pageRightLowerSprite.sprite = spriteList[currentPage + 2];
                if(outlinePage == currentPage + 2){
                    pageRightOutline.SetActive(true);
                    outlineCaseValue = 0; // right lower is new page CASE 0
                }
            }


            // RIGHT UNDER
            if(spriteList.Count <= (currentPage + 3)) { // no page found
                //pageRightUnderSprite.enabled = false;
                pageRightUnder.SetActive(false);
            } else { // page found
                pageRightUnder.SetActive(true);
                pageRightUnderSprite.enabled = true;
                pageRightUnderSprite.sprite = spriteList[currentPage + 3];
                if(outlinePage == currentPage + 3){
                    pageRightUnderOutline.SetActive(true);
                }
            }
            
            
        }
    }

    public void addPage(Sprite page){
        spriteList.Add(page);
        changePages();
        Debug.Log("ADDED PAGE TO NOTEBOOK");
    }

    public void conditionToEndNotif(int caseValue){ // end highlighting/outlining of new page
        if(bookLeftHingeJoint.angle < -80){
            return; // don't end anything if book is slightly closed
        }
        // if right lower sprite is new. then right page must be at least > 130

        // if sprite upper right is new, right page angle must be < 50

        // if left upper is new, then left page must be > -40

        Debug.Log("case value: " + caseValue);
        if(!outlineSparkles.isPlaying){
                outlineSparkles.Play();
        } 

        switch (caseValue)
        {
            case 0: // right lower is new
                if (pageRightHingeJoint.angle > 130){
                    outlinePage = -1;
                    outlineCaseValue = -1;
                }
                break;
            case 1: // right upper is new
                if (pageRightHingeJoint.angle < 50){
                    outlinePage = -1;
                    outlineCaseValue = -1;
                }
                break;
            case 2: // left upper is new
                if (pageLeftHingeJoint.angle > -40){
                    outlinePage = -1;
                    outlineCaseValue = -1;
                }
                break;
            default: // random input or -1 case value??
                Debug.Log("error invalid input to end");
                break;
        }

    }

    public bool bookClosed(){
        if(bookLeftHingeJoint.angle < -170){
            return true;
        }
        return false;
    }
}
