Expense Tracker Application : SpendWise 

Student: Vidhi Hardik Patira
Institute : Vanier College of Continuing Education, QC, Canada
Instructor: Mrs. Silviya Paskleva

MindMap:
Here is SpendWise mapped to each phase:
1. Requirement Gathering
•	Users want to track daily expenses
•	Categorize expenses (Food, Travel, Bills, etc.)
•	View monthly spending summary
•	Add income
•	Set budgets
•	Export reports (PDF/Excel)

2.Functional requirements (the features that the system should support)
•	User account management
-Users should be able to sign-up, login and logout
-Support password reset
-Users should be able to manage their accounts 

•	Expenses management
-Users should be able to add new expenses with amount, category (family expenses, grocery, vacation, etc.), data & time, others (maybe notes).
-Users should be able to Add, delete and edit expenses.


•	Notification or Reminder
-Reminder to log expenses
-Alerts: Budget limits or unusual spending

•	Reporting Analytics
-Should provide visualizations (example: graphics or charts) to help users understand their financial situation.


•	Data Security
-Secure Authentication
-Backup and restore

3.Non-Functional Requirements (how well this app should work rather than what it should do)

•	Performance
-App should load the dashboard within ≤ 2 seconds
-App should have a quick response to entering, searching and retrieving expense data.

•	Scalability
-Support growth to millions of users


•	Security
-Encrypt sensitive data (in transit and at rest)
-secure login 
-protection against unauthorized access

•	Backup and recovery
-Should automatically backup to cloud

•	Supportability
-Easy updates 
- Error logs 

•	Compatibility
-Ensure compatibility with various devices (example: mobile or desktops)


4.Architecture & Design
SpendWise is a 3 tier application, matching your document:
“Web applications is a very common example of 3 tier applications.”
So SpendWise tiers:
Tier	Components
Presentation	HTML, CSS, JS, React/Angular
Application Logic	C#
Data Tier	PostgreSQL

5.Implementation
•	Backend APIs for expenses, income, categories
•	Frontend UI screens
•	Database tables (Users, Expenses, Income, Categories)

6.Testing
•	Functional Testing
•	Domain Testing
•	Login Valid/Invalid
•	Add Expense Valid/Invalid
•	Stress/Load Testing
•	Report Generation Testing

7.Deployment
•	Cloud Deployment (AWS/Azure)
•	Database Setup
•	Version Release
•	Backup & Recovery

8.Maintenance
•	Bug Fixes
•	New Features
•	Performance Optimization
•	User Support

<img width="952" height="807" alt="image" src="https://github.com/user-attachments/assets/14d64235-0cc8-4a37-8d1f-8f6ee10ca51a" />
PostgreSQL CREATE TABLE Scripts (Full Schema)
1. USERS TABLE
CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

2. CATEGORIES TABLE
CREATE TABLE categories (
    category_id SERIAL PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL,
    type VARCHAR(20) CHECK (type IN ('Expense', 'Income')) NOT NULL
);

3. INCOME SOURCES TABLE
CREATE TABLE income_sources (
    source_id SERIAL PRIMARY KEY,
    source_name VARCHAR(100) NOT NULL
);

4. EXPENSES TABLE
CREATE TABLE expenses (
   expense_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    category_id INT NOT NULL REFERENCES categories(category_id),
    amount NUMERIC(10,2) NOT NULL CHECK (amount > 0),
    description TEXT,
    payment_method VARCHAR(50),
    expense_date DATE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

5. INCOME TABLE
CREATE TABLE income (
    income_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    source_id INT NOT NULL REFERENCES income_sources(source_id),
    category_id INT NOT NULL REFERENCES categories(category_id),
    amount NUMERIC(10,2) NOT NULL CHECK (amount > 0),
    description TEXT,
      income_date DATE NOT NULL,
      created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

6. BUDGETS TABLE

CREATE TABLE budgets (
    budget_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    category_id INT NOT NULL REFERENCES categories(category_id),
    amount_limit NUMERIC(10,2) NOT NULL CHECK (amount_limit > 0),
    month_year VARCHAR(7) NOT NULL  -- Format: YYYY-MM
);

INSERT VALUES:
1.	Users 
INSERT INTO users (full_name, email, password_hash) VALUES
('Lina Moreau', 'lina.moreau@email.com', 'hash123'),
('Om Patel', 'om.patel@email.com', 'hash456'),
('Sophie Laurent', 'sophie.laurent@email.com', 'hash789');

2.	Categories :

INSERT INTO categories (category_name, type) VALUES
('Food & Dining', 'Expense'),
('Transportation', 'Expense'),
('Entertainment', 'Expense'),
('Salary', 'Income'),
('Freelancing', 'Income'),
('Investments', 'Income');

3.	Income Sources:
INSERT INTO income_sources (source_name) VALUES
('Full-Time Job'),
('Freelance Work'),
('Stock Dividends');

4.	Expenses:

INSERT INTO expenses (user_id, category_id, amount, description, payment_method, expense_date) VALUES
(1, 1, 25.50, 'Lunch at cafe', 'Credit Card', '2026-04-10'),
(2, 2, 60.00, 'Monthly bus pass', 'Debit Card', '2026-04-11'),
(3, 3, 15.99, 'Movie ticket', 'Cash', '2026-04-12');
5.	Income:

INSERT INTO income (user_id, source_id, category_id, amount, description, income_date) VALUES
(1, 1, 4, 3200.00, 'Monthly salary', '2026-04-01'),
(2, 2, 5, 450.00, 'Freelance website design', '2026-04-05'),
(3, 3, 6, 120.00, 'Dividend payout', '2026-04-07');

6.	Budgets:
     
 INSERT INTO budgets (user_id, category_id, amount_limit, month_year) VALUES
(1, 1, 300.00, '2026-04'),
(2, 2, 150.00, '2026-04'),
(3, 3, 100.00, '2026-04');



