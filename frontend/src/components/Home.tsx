interface HomeProps {
  onLogout: () => void;
}

const Home = (props: HomeProps) => {
  return (
    <div>
      <h1>Welcome to the Home Page!</h1>
      <p>
        This is a protected route. You can only see this if you're logged in.
      </p>
    </div>
  );
};
export default Home;
