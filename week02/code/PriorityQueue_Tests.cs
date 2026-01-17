using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue one item and dequeue it
    // Expected Result: Returns the enqueued item
    // Defect(s) Found: None
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("A", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities, dequeue should return highest priority
    // Expected Result: Returns "B" first (priority 3), then "A" (priority 1)
    // Defect(s) Found: Dequeue does not remove the item from the queue.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        var result1 = priorityQueue.Dequeue();
        Assert.AreEqual("B", result1);
        var result2 = priorityQueue.Dequeue();
        Assert.AreEqual("A", result2);
    }

    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: Throws InvalidOperationException
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyDequeue()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(priorityQueue.Dequeue);
    }

    [TestMethod]
    // Scenario: Enqueue items with same priority, dequeue should return first enqueued
    // Expected Result: Returns "A" first, then "B"
    // Defect(s) Found: Dequeue does not remove the item from the queue.
    public void TestPriorityQueue_SamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 2);
        var result1 = priorityQueue.Dequeue();
        Assert.AreEqual("A", result1);
        var result2 = priorityQueue.Dequeue();
        Assert.AreEqual("B", result2);
    }

    // Add more test cases as needed below.
}