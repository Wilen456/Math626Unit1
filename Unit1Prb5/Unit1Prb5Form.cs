/************************************************************/
/*                                                          */
/*  Class: MATH 626 - Data Structures and Algorithms        */
/*                                                          */
/*  Program: Unit1Prb5.cs                                   */
/*                                                          */
/*  Programmer: Griffin Poelling                            */
/*                                                          */
/*  Purpose: Test LinkedQueueLib with simple job queue      */
/*           application                                    */
/*                                                          */
/************************************************************/
using LinkedQueueLib;

namespace Unit1Prb5
{
    public class LinkedQueue
    {
        private QueueNode head;
        private QueueNode tail;
        private int size;
        private string Name;
        
        public LinkedQueue()
        {
            head = null;
            tail = null;
            size = 0;
            Name = string.Empty;
        }
        public LinkedQueue(string name)
        {
            head = null;
            tail = null;
            size = 0;
            Name = name;
        }
        public string name()
        {
            return Name;
        }
        public int Size()
        {
            return size;
        }
        public bool Empty()
        {
            if (size == 0)
            {
                return true;
            }
            else return false;
        }
        public void Enqueue(object data)
        {
            QueueNode newNode = new QueueNode(data);
            if (this.Empty())
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
            size++;
        }
        public object Dequeue()
        {
            if (this.Empty())
                throw new InvalidOperationException("Queue is empty.");

            object data = head.Data;
            head = head.Next;
            size--;

            // If the queue is empty, reset the tail as well
            if (this.Empty())
                tail = null;

            return data;
        }
        public QueueNode Peek()
        {
            if (this.Empty())
                return null;
            return head;
        }
        public void Purge()
        {
            head = null;
            tail = null;
            size = 0;
        }
    }

    public class QueueNode
    {
        public object Data { get; set; }
        public QueueNode Next { get; set; }
        public QueueNode(object data)
        {
            Data= data;
            Next = null;
        }
    }
    public partial class Unit1Prb5Form : Form
    {
         public LinkedQueue jobQueue = new LinkedQueue("Job Queue");
        public Unit1Prb5Form()
        {
            InitializeComponent();
        }

        private void newJobButton_Click(object sender, EventArgs e)
        {
            
            Job newJob = new Job();
            addForm addDialog = new addForm();
            ;

            if (addDialog.ShowDialog()  == DialogResult.OK)
            {
                newJob.JobNumber = addDialog.returnJobNum;
                newJob.UserName = addDialog.returnUName;

                jobQueue.Enqueue(newJob);
            }

            PrintQueue();
            
        }

        private void releaseJobButton_Click(object sender, EventArgs e)
        {
            
            if (!jobQueue.Empty())
            {
                Job releasedJob;
                releasedJob = (Job)jobQueue.Dequeue();
                MessageBox.Show("Job number " + releasedJob.JobNumber.ToString() + " for " + releasedJob.UserName + " completed.");
                PrintQueue();
            }
            else
            {
                MessageBox.Show("Job queue is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        private void PrintQueue()
        {
            
            QueueNode currentNode;
            Job currentJob;

            numTextBox.Text = jobQueue.Size().ToString();

            queueTextBox.Clear();

            currentNode = jobQueue.Peek();

            while (currentNode != null)
            {
                currentJob = (Job) currentNode.Data;
                queueTextBox.Text += currentJob.ToString();
                queueTextBox.Text += System.Environment.NewLine;
                currentNode = currentNode.Next;
            }
            
        }

    }
    public class Job
    {
        private int jobNumber;
        private string userName;

        public Job()
        {
            this.jobNumber = 0; this.userName = "";
        }

        public int JobNumber // Define read/write JobNumber property
        {
            get
            {
                return this.jobNumber;
            }
           
            set
            {
                this.jobNumber = value;
            }
        }

        public string UserName // Define read/write UserName property
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
            }
        }

        public override string ToString()
        {
            return this.jobNumber.ToString() + ", " + this.userName;
        }
    }
}