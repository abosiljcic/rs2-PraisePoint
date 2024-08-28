export interface Post {
  id: string;
  senderUsername: string;
  receiverUsername: string;
  companyId: string;
  points: number;
  description: string;
  postComments: {
    id: string;
    username: string;
    text: string;
  }[];
  postLikes: {
    id: string;
    username: string;
  }[];
}
