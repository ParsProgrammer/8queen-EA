# 8 Queen Problem - Genetic Algorithm

The **8 Queen Problem** involves placing 8 queens on a chessboard so that no two queens attack each other.  
- Each queen must be placed in a unique row and column.  
- The main challenge is **minimizing diagonal attacks**.  

In this project, we solve the problem using a **Genetic Algorithm (GA)** approach.

![Chessboard Example](https://github.com/ParsProgrammer/8queen-EA/blob/master/Picture1.png?raw=true)

---

## 🧩 Components of the Genetic Algorithm

### 1. Individual (Chromosome)
Each individual represents a potential solution (placement of queens) on the board.

![Individual Chromosome](https://github.com/ParsProgrammer/8queen-EA/blob/master/ind.png?raw=true)

---

### 2. Population
A collection of individuals. The population evolves over generations to improve solutions.

![Population](https://github.com/ParsProgrammer/8queen-EA/blob/master/pop.png?raw=true)

---

### 3. Fitness Function
The fitness function evaluates how good an individual is.  
- It counts the number of diagonal conflicts.  
- Solutions with fewer conflicts have higher fitness.

![Fitness Function](https://github.com/ParsProgrammer/8queen-EA/blob/master/fitness.png?raw=true)

---

### 4. Output
After several generations, the algorithm converges to a solution where no queens attack each other.

![Output Solution](https://github.com/ParsProgrammer/8queen-EA/blob/master/Picture2.png?raw=true)

---

## ⚙️ How It Works
1. **Initialize** a population of random solutions.  
2. **Evaluate** fitness of each individual.  
3. **Select** the fittest individuals for reproduction.  
4. **Crossover and mutate** to produce the next generation.  
5. **Repeat** until a solution with zero conflicts is found or a max number of generations is reached.

---

## 📚 Notes
- This approach demonstrates how **evolutionary algorithms** can solve combinatorial optimization problems.  
- The algorithm can be extended to N-Queens or other constraint-satisfaction problems.
